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
                    <td style="border-right: none;" class="sticky-col header-sticky-col">CHỨC NĂNG</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(employee, index) in employees" :key="index" 
                @dblclick="editEmployee(employee)" 
                @mouseover="colorHover[index] = true" @mouseleave="colorHover[index] = false">
                    <td style="border-left: none;" :class="{'text-center': style.center}"><input type="checkbox" name="" id=""></td>
                    <td>{{employee.EmployeeCode}}</td>
                    <td>{{employee.FullName}}</td>
                    <td>{{employee.GenderName}}</td>
                    <td :class="{'text-center': style.center}">{{format(employee.DateOfBirth)}}</td>
                    <td :class="{'text-right': style.right}">{{employee.IdentityNumber}}</td>
                    <td>{{employee.PositionName}}</td>
                    <td>{{employee.DepartmentName}}</td>
                    <td>{{employee.PersonalTaxCode}}</td>
                    <td>{{employee.QualificationName}}</td>
                    <td>{{employee.EducationalBackground}}</td>
                    <td class="sticky-col" style="border-right: none" :ref="'col_'+index" v-on:dblclick.stop :class="{'color-hover':colorHover[index]}">
                        <div class="tool">
                            <div class="textTool">Sửa</div>
                            <div class="icon-drop" 
                            @click="getPositionTop(index)"
                            :class="{'icon-drop-click': isShowDataDropList[index]}"></div>

                        </div>
                    </td>
                      
                    <MDropList  v-show="isShowDataDropList[index]" :style="{'top': `${topDropList}px`}"  
                    :dataDropList = "dataDropList" @tool = 'tool' :emp='employee'/> 
                </tr>  
                            
            </tbody>
        </table>
        <!-- @click="getPositionTop(isShowDataDropList[index], index)"
                            :class="{'icon-drop-click': isShowDataDropList[index]}"></div> -->
    </div>

</template>


<script>
import MDropList from '../../base/MDropList.vue';
import format from '@/common/formatData';
    async function getUserAsync() {
        let response = await fetch('https://cukcuk.manhnv.net/api/v1/Employees');
        let data = await response.json()
        return data;
    }

  export default {
    created() {
        getUserAsync().then(data =>{
            Object.assign(this.employees, data);
        });
        
    },
    updated() {
        this.$emit("getEmployees", this.employees);
    },
    methods: {

        /**
         * Hàm format date ngày/tháng/năm
         * author: (14/9/2022)
         * @param {String} date 
         */
        format(date){
            return format.formatDate(date);
        },
        /**
        * Đưa dữ liệu lên form để sửa
        * author: LTQN(10/9/2022)
        */
        editEmployee(employee) {
            this.emp = employee;
            this.$emit("editEmployee", this.emp);
        },
        /**
         * Hàm xóa/ nhân bản/ ngừng sử dụng
         * author: LTQN (15/9/2022)
         */
        tool(e){
            if(e.tool == this.toolDropList.Delete){
                this.$emit('showPopUp', e.emp.EmployeeId);
            }
        },

        /**
         * author: LTQN (14/9/2022)
         * Hàm set top cho droplist
         * @param {int} index : vị trí trong mảng employee
         */
        getPositionTop(index){
            this.isShowDataDropList[index] = !this.isShowDataDropList[index];
            let [td] = this.$refs['col_'+index];
            this.topDropList = td.getBoundingClientRect().top + 35;
        },

        setColorHover(){
            this.colorHover = true;
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
            isShowDataDropList: [],
            dataDropList: [
                {
                    type: 'Duplicate',
                    text: 'Nhân bản'
                },
                {
                    type: 'Delete',
                    text: 'Xóa'
                },
                {
                    type: 'Stop',
                    text: 'Ngừng sử dụng'
                }
            ],
            toolDropList: {
                Duplacate: 'Duplicate',
                Delete: 'Delete',
                Stop: 'Stop'
            },
            topDropList: null,
            colorHover: [] //set màu sắc hover cho col sticky
        };
    },
    components: { MDropList}
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




