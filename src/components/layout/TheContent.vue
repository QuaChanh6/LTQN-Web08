<template>
    <div class="content">
        <div class="content-header">
            <div class="title">Nhân viên</div>
            <div class="tool-bar">
                <!-- <button class="button add-employee">Thêm mới nhân viên</button> -->
                <MButton class="add-employee" :text= "'Thêm mới nhân viên'" @click="addEmployee()"/>
                <!-- <MSCombobox :url="'https://cukcuk.manhnv.net/api/v1/Positions'" :propValue='PositionId' :propText='PositionName'/> -->
            </div>
        </div>
        <div class="container-grid">
            <div class="filter">
                <div class="search input-icon">
                    <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên">
                    <div class="icon-search"></div>
                </div>
                <div class="refresh" @click="reload"></div>
            </div>
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

            <ThePagination :TotalEmployee="employees.length"/>
        </div>
        
    </div>
    <TheForm  v-if="showForm" @closeForm='closeForm' :employee="emp" :mode='formMode' @reload='reload'/>
    <MPopup v-if="isShowFopup" :id='deleteEmployeeID' @closePopup='closePopup' @deleteEmp='deleteEmp'/>
 
</template>

<script>
import ThePagination from './ThePagination.vue';
import MButton from '../base/MButton.vue';
import MDropList from '../base/MDropList.vue';
import TheForm from '../view/employee/TheForm.vue';
import MPopup from '../base/MPopup.vue';


export default {
    components: { ThePagination, MButton, MDropList, TheForm, MPopup },
    created(){
        fetch("https://cukcuk.manhnv.net/api/v1/Employees")
        .then(res => res.json())
        .then(res => {
            this.employees=res;
        })
    },
    methods: {
        /**
         * Hàm forrmat date
         * @param {date} dateSrc 
         * author: LTQN(9/9/2022)
         */
        formatDate(dateSrc){
            if(dateSrc != null){
                let dateString = dateSrc.slice(0,10);
                let date = new Date(dateString);
                let year = date.getFullYear().toString(),
                month = (date.getMonth() + 1).toString().padStart(2, '0'),
                day = date.getDate().toString().padStart(2, '0');
                return `${day}/${month}/${year}`;
            }
           
            return '';
            
        },
        /**
         * Ham đóng form
         * author: LTQN(9/9/2022)
         */
        closeForm(){
            this.showForm = false;
        },
        /**
         * Hiện popup xóa
         * author: LTQN(10/9/2022)
         */
        showPopUp(e){
            this.deleteEmployeeID=e;
            this.isShowFopup = true;
        },
        /**
         * Đưa dữ liệu lên form để sửa
         * author: LTQN(10/9/2022)
         */
        editEmployee(employee){
            this.emp = employee;
            this.showForm = true;
            this.formMode = this.modeOfForm.EDIT;
        },
        /**
         * Ham mở form khi thêm mới
         * author: LTQN(10/9/2022)
         */
        addEmployee(){
            this.emp = {};
            this.showForm = true;
            this.formMode = this.modeOfForm.ADD;
        },
        /**
         * hàm đóng popUp
         * author: LTQN(10/9/2022)
         */
        closePopup(){
            this.isShowFopup = false;
        },
        /**
         * hàm xóa nhân viên
         * author: LTQN(10/9/2022)
         */
        deleteEmp(e){
            fetch('https://cukcuk.manhnv.net/api/v1/Employees/' + e, {method: 'DELETE'})
            .then(res => res.json())
            .then(res => {
                //load lại trang
                let randomKey = Math.floor(Math.random()*10000);
                this.$emit('loadContent', randomKey);
                console.log('Thành công'+ res);
            }).catch((error) => {
               console.error('Error:', error);
            })
            this.isShowFopup = false;
        },
        /**
         * Hàm reload lại table
         * author: LTQN(10/9/2022)
         */
        reload(){
            let randomKey = Math.floor(Math.random()*10000);
            this.$emit('loadContent', randomKey);
        }
    },
    data() {
        return{
            employees: [],
            showForm: false,
            emp: {},
            isShowFopup: false,
            deleteEmployeeID: '',
            modeOfForm: {
                ADD: 1,
                EDIT: 2
            },
            formMode: null,
            style: {
                center: true,
                right: true
            },
        }
    },
    
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/content.css);
@import url(../../css/base/table.css);
.text-center{
    text-align: center;
}
.text-right{
    text-align: right;
}
</style>
