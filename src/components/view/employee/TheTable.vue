<template>
    <div class="container-table">
        <table>
            <thead>
                <tr>
                    <th class="checkbox" :class="{'text-center': style.center}"><input type="checkbox" name="" id=""></th>
                    <th class="employeeCode">MÃ NHÂN VIÊN</th>
                    <th class="employeeName">TÊN NHÂN VIÊN</th>
                    <th class="genderName">GIỚI TÍNH</th>
                    <th class="dateOfBirth" :class="{'text-center': style.center}">NGÀY SINH</th>
                    <th class="identityNumber" :class="{'text-right': style.right}"><MTooltip :content="'Chứng minh nhân dân'" :stand="'down'"/>
                        SỐ CMND
                    </th>
                    <th class="position">CHỨC DANH</th>
                    <th class="departmentName">TÊN ĐƠN VỊ</th>
                    <th class="accountBank">SỐ TÀI KHOẢN</th>
                    <th class="nameOfBank">TÊN NGÂN HÀNG</th>
                    <th class="branchOfBank">CHI NHÁNH TÀI KHOẢN NGÂN HÀNG</th>
                    <th class="sticky-col header-sticky-col">CHỨC NĂNG</th>
                </tr>
            </thead>
            <tbody>
                
                <tr v-for="(employee, index) in employees" :key="index" 
                @dblclick="editEmployee(employee)">
                    <td class="checkbox" :class="{'text-center': style.center}"><input type="checkbox" name="" id=""></td>
                    <td class="employeeCode">{{employee.employeeCode}}</td>
                    <td class="employeeName">{{employee.employeeName}}</td>
                    <td class="genderName">{{employee.genderName}}</td>
                    <td class="dateOfBirth" :class="{'text-center': style.center}">{{format(employee.dateOfBirth)}}</td>
                    <td class="identityNumber" :class="{'text-right': style.right}">{{employee.identityNumber}}</td>
                    <td class="position">{{employee.position}}</td>
                    <td class="departmentName">{{employee.departmentName}}</td>
                    <td class="accountBank">{{employee.accountBank}}</td>
                    <td class="nameOfBank">{{employee.nameOfBank}}</td>
                    <td class="branchOfBank">{{employee.branchOfBank}}</td>
                    <td class="sticky-col" @click="getPositionTop(index)" :ref="'col_'+index" v-on:dblclick.stop>
                        <div class="tool">
                            <div class="textTool">Sửa</div>
                            <div class="icon-drop" 
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
import MTooltip from '@/components/base/MTooltip.vue';

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
        searchEmployee : String,
        pageSize: Number,
        pageNumber: Number
    },
    created() {
        try {
            let url = process.env.VUE_APP_URL+'Employees/filter?limit='+this.pageSize+'&pageNumber='+this.pageNumber;
            this.$emit('loading', true);
            if(!format.checkEmptyData(this.searchEmployee)){ // thực hiện tìm kiếm
                url = url +'&keyword=' + this.searchEmployee;
            }
            getUserAsync(url).then(data =>{
                this.$emit('loading', false);
                this.$emit('dataFromTable', {TotalRecord: data.totalCount, TotalPage: Math.ceil(data.totalCount/this.pageSize)});
                this.employees = data.data;
            });
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
                if(e.tool === Resource.toolDropList.Delete){ //nếu là xóa
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
        };
    },
    components: { MDropList, MTooltip }
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




