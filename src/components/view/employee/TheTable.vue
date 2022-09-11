<template>
    <div class="container-table">
        <table>
            <thead>
                <tr>
                    <td style="border-left: none;" :class="{'text-center': style.center}"><input type="checkbox" name="" id=""></td>
                    <td>MÃ NHÂN VIÊN</td>
                    <td>TÊN NHÂN VIÊN</td>
                    <td>GIỚI TÍNH</td>
                    <td :class="{'text-center': style.center}">NGÀY SINH</td>
                    <td :class="{'text-right': style.right}">SỐ CMND</td>
                    <td>CHỨC DANH</td>
                    <td>TÊN ĐƠN VỊ</td>
                    <td>SỐ TÀI KHOẢN</td>
                    <td>TÊN NGÂN HÀNG</td>
                    <td>CHI NHÁNH TÀI KHOẢN NGÂN HÀNG</td>
                    <td style="border-right: none">CHỨC NĂNG</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(employee, index) in employees" :key="index" @dblclick="editEmployee(employee)">
                    <td style="border-left: none;" :class="{'text-center': style.center}"><input type="checkbox" name="" id=""></td>
                    <td>{{employee.EmployeeCode}}</td>
                    <td>{{employee.FullName}}</td>
                    <td>{{employee.GenderName}}</td>
                    <td :class="{'text-center': style.center}">{{formatDate(employee.DateOfBirth)}}</td>
                    <td :class="{'text-right': style.right}">{{employee.IdentityNumber}}</td>
                    <td>{{employee.PositionName}}</td>
                    <td>{{employee.DepartmentName}}</td>
                    <td>{{employee.PersonalTaxCode}}</td>
                    <td>{{employee.QualificationName}}</td>
                    <td>{{employee.EducationalBackground}}</td>
                    <td style="border-right: none">
                        <MDropList :employeeID='employee.EmployeeId'  @showPopUp="showPopUp"/>
                    </td>
                </tr>
                            
            </tbody>
        </table>
    </div>
</template>
  
  <script>
import MDropList from '@/components/base/MDropList.vue';
  export default {
    created() {
        fetch("https://cukcuk.manhnv.net/api/v1/Employees")
            .then(res => res.json())
            .then(res => {
            this.employees = res;
        });
    },
    mounted() {
        this.$emit("getEmployees", this.employees);
    },
    methods: {
        /**
         * Hàm forrmat date
         * @param {date} dateSrc
         * author: LTQN(9/9/2022)
         */
        formatDate(dateSrc) {
            if (dateSrc != null) {
                let dateString = dateSrc.slice(0, 10);
                let date = new Date(dateString);
                let year = date.getFullYear().toString(), month = (date.getMonth() + 1).toString().padStart(2, "0"), day = date.getDate().toString().padStart(2, "0");
                return `${day}/${month}/${year}`;
            }
            return "";
        },
        /**
        * Đưa dữ liệu lên form để sửa
        * author: LTQN(10/9/2022)
        */
        editEmployee(employee) {
            this.emp = employee;
            this.$emit("editEmployee", this.emp);
        },
        showPopUp(e){
            this.$emit('showPopUp', e);
        }
    },
    data() {
        return {
            employees: [],
            emp: {},
            style: {
                center: true,
                right: true
            },
        };
    },
    components: { MDropList }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
    @import url(../../../css/base/table.css);
    .text-center{
    text-align: center;
    }
    .text-right{
        text-align: right;
    }
  </style>