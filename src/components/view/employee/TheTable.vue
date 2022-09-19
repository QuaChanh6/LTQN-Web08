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
                    <td  style="border-right: none;">CHI NHÁNH TÀI KHOẢN NGÂN HÀNG</td>
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
                    <td style="border-right: none">{{employee.EducationalBackground}}</td>
                    <td class="sticky-col" :ref="'col_'+index" v-on:dblclick.stop :class="{'color-hover':colorHover[index]}">
                        <div class="tool">
                            <div class="textTool">Sửa</div>
                            <div class="icon-drop" 
                            @click="getPositionTop(index)"
                            :class="{'icon-drop-click': isShowDataDropList[index]}"></div>

                        </div>
                    </td>
                      
                    <MDropList  v-show="isShowDataDropList[index]" :style="{'top': `${topDropList}px`}"  
                    @tool = 'tool' :emp='employee' :dataDropList = 'dataDropList'/> 
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
import Resource from '../../../common/Resource';

    async function getUserAsync(url) {
        try {
            let response = await fetch(url);
            let data = await response.json();
            return data;
        } catch (error) {   
            console.log(error);
            return [];
        }
        
    }

  export default {
    props: {
        searchEmployee : String
    },
    created() {
        try {
            let url = '';
            this.$emit('loading', true);
            if(this.searchEmployee != ''){ // thực hiện tìm kiếm
                url = Resource.Url+'Employees/filter?employeeFilter=' + this.searchEmployee;
                getUserAsync(url).then(data =>{
                    this.$emit('loading', false)
                    Object.assign(this.employees, data.Data);
                })
            }else{ //Hiển thị tất cả nhân viên
                url = Resource.Url+'Employees';
                getUserAsync(url).then(data =>{
                    this.$emit('loading', false)
                    Object.assign(this.employees, data);
                });
            }
        } catch (error) {
            console.log(error);
        }
        
       
    },
    updated() {
        this.$emit("getEmployees", this.employees); //tahy đổi dữ liệu -> cập nhật lại số bản ghi
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
            try {
                this.emp = employee;
                this.$emit("editEmployee", this.emp);
            } catch (error) {
                console.log(error);
            }

        },
        /**
         * Hàm xóa/ nhân bản/ ngừng sử dụng
         * author: LTQN (15/9/2022)
         */
        tool(e){
            try {
                if(e.tool == Resource.toolDropList.Delete){ //nếu là xóa
                this.$emit('showPopUp', e.emp);
                this.isShowDataDropList = [];
            } 
            } catch (error) {
                console.log(error);
            }
            
        },

        /**
         * author: LTQN (14/9/2022)
         * Hàm set top cho droplist
         * @param {int} index : vị trí trong mảng employee
         */
        getPositionTop(index){
            try {
                this.isShowDataDropList[index] = !this.isShowDataDropList[index];
                let td = this.$refs['col_'+index];
                this.topDropList = td[0].getBoundingClientRect().top + 35;
                // console.log(td);
                // console.log([td]);
            } catch (error) {
                console.log(error);
            }

        },
        /**
         * Hàm set màu khi hover của cột sticky
         * authorL LTQN(16/9/2022)
         */
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
            dataDropList: Resource.dataDropList,
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




