<template>

    <div class="container-table">
        <table  @mousemove="getPosition" class="table-emp">
            <thead>
                <tr>
                    <th class="checkbox" :class="{'text-center': style.center}">
                        <input type="checkbox" :value="pageNumber" v-model="checkedAll" @click='clickAll'>
                    </th>
                    <th class="employeeCode sort">
                        <div>MÃ NHÂN VIÊN</div>
                        <div class="icon-sort">
                            <div class="icon-asc" @click="asc('employeeCode')"></div>
                            <div class="icon-desc" @click="desc('employeeCode')"></div>
                        </div>
                    </th>
                    <th class="employeeName sort">
                        <div>TÊN NHÂN VIÊN</div>
                        <div class="icon-sort">
                            <div class="icon-asc" @click="asc('employeeName')"></div>
                            <div class="icon-desc" @click="desc('employeeName')"></div>
                        </div>
                    </th>
                    <th class="genderName">GIỚI TÍNH</th>
                    <th class="dateOfBirth" :class="{'text-center': style.center}">NGÀY SINH</th>
                    <th class="identityNumber" @mouseleave="hideTooltip" @mouseenter="showTooltip('Số chứng minh nhân dân')" :class="{'text-right': style.right}">
                        SỐ CMND</th>
                    <th class="position">CHỨC DANH</th>
                    <th class="departmentName">TÊN ĐƠN VỊ</th>
                    <th class="address">ĐỊA CHỈ</th>
                    <th class="email">EMAIL</th>
                    <th class="status">TÌNH TRẠNG CÔNG VIỆC</th>
                    <th class="header-sticky-col sticky-col">CHỨC NĂNG</th>
                </tr>
            </thead>           
            <tbody ref="bodyTable">
                <tr v-for="(employee, index) in employees" :key="index" 
                @dblclick="editEmployee(employee.employeeID, modeEdit)" @click = 'chooseTr = index' :class="{'chooseTr': chooseTr == index}">
                    <td class="checkbox" :class="{'text-center': style.center}" v-on:click.stop v-on:dblclick.stop>
                        <input type="checkbox" :value="employee.employeeID" v-model="checkedEmployees"></td>
                    <td class="employeeCode">{{employee.employeeCode}}</td>
                    <td class="employeeName">{{employee.employeeName}}</td>
                    <td class="genderName">{{employee.genderName}}</td>
                    <td class="dateOfBirth" :class="{'text-center': style.center}">{{format(employee.dateOfBirth)}}</td>
                    <td class="identityNumber" :class="{'text-right': style.right}">{{employee.identityNumber}}</td>
                    <td class="position">{{employee.positionName}}</td>
                    <td class="departmentName">{{employee.departmentName}}</td>
                    <td class="address">{{employee.address}}</td>
                    <td class="email">{{employee.email}}</td>
                    <td class="status">{{employee.statusName}}</td>
                    <td class="sticky-col" :ref="'col_'+index" v-on:dblclick.stop>
                        <div class="tool">
                            <div class="textTool" @click="editEmployee(employee.employeeID, modeEdit)">Sửa</div>
                            <div class="icon-drop" 
                            :class="{'icon-drop-click': isShowDataDropList[index]}" 
                            @click="getPositionTop(index)"></div>

                        </div>
                    </td>
                      
                    <MDropList  v-if="isShowDataDropList[index]" :style="{'top': `${topDropList}px`}" @closeDropList="closeDropList"
                    @tool = 'tool' :emp='employee' :dataDropList = 'dataDropList' :index = 'index'/> 
                </tr>  
                            
            </tbody>
        </table>
        <div class="loadingTableLazy" v-show="isLoadTable"><div v-for="i in 10" :key="i" class="container-skeleton">
            <div  class="skeleton" v-for="i in 5" :key="i"></div>
        </div>
    </div>
        <!-- @click="getPositionTop(isShowDataDropList[index], index)"
                            :class="{'icon-drop-click': isShowDataDropList[index]}"></div> -->
    </div>

</template>


<script>
import MDropList from '../../base/MDropList.vue';
import format from '@/common/formatData';
import resource from '../../../common/resource';
import enumeration from '@/common/enumeration';

/**
 * Lấy thông tin nhân viên
 * author: LTQN(10/9/2022)
 * @param {string} url 
 */
    async function getUserAsync(url, me) {
        try {
            let response = await fetch(url);
            if(response.ok){
                let data = await response.json();
                return data;

            }else{//lỗi
                me.$emit('openToast', resource.ToastMessageType.error, resource.ToastMessage.error);
                return [];
            }
        } catch (error) {   
            console.log(error);
            return [];
        }
        
    }

  export default {
    props: {
        searchEmployee : String,
        pageSize: Number,
        pageNumber: Number,
        listIdDelete: Array,
        listCheckAllOfPage: Array,
        conditionPos: String,
        conditionDep: String,
    },
    created() {
        try {
            //checkbox
            this.checkedEmployees = this.listIdDelete;
            this.listCheckAllOfPage.filter((page) => {
                this.checkedAll.push(page);
            })

            const me = this;
            let url = process.env.VUE_APP_URL+'Employees/filter?limit='+this.pageSize+'&pageNumber=';

            
            if(sessionStorage.getItem("page") != undefined){
                url = url + Number(sessionStorage.getItem("page"));
            }else url = url + this.pageNumber;
            if(sessionStorage.getItem("role") == '2'){
                url = url +'&department=' + sessionStorage.getItem("managedep").toString();
            }else{
                if(!format.checkEmptyData(this.conditionDep)){
                url = url +'&department=' + this.conditionDep;
            }
            }
            
            this.isLoadTable = true;
            if(!format.checkEmptyData(this.searchEmployee)){ // thực hiện tìm kiếm
                url = url +'&keyword=' + this.searchEmployee;
            }

            if(!format.checkEmptyData(this.conditionPos)){
                url = url +'&position=' + this.conditionPos;
            }
            
            this.urlTemp = url;
            getUserAsync(url, me).then(data =>{
                this.isLoadTable = false;
                this.$emit('dataFromTable', {TotalRecord: data.totalCount, TotalPage: Math.ceil(data.totalCount/this.pageSize)});
                this.employees = data.data;


            })
        } catch (error) {
            console.log(error);
        }
        
       
    },
    mounted(){
        this.$refs.bodyTable.addEventListener('scroll', this.getPositionLeft);
    },
    updated() {
        this.$emit("getEmployees", this.employees); //tahy đổi dữ liệu -> cập nhật lại số bản ghi
    },
    beforeUnmount(){
        this.$refs.bodyTable.removeEventListener('scroll', this.getPositionLeft);
    },
    methods: {
       
        /**
         * Lấy  vị trí thanh cuộn ngang
         * author: LTQN(8/10/2022)
         * @param {*} e : scroll
         */
        getPositionLeft(e){
            this.positionScrollLeft = e.target.scrollLeft;
        },
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
        editEmployee(employeeId, mode) {
            let url = process.env.VUE_APP_URL + "Employees/" + employeeId;
            fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.emp = res[0];              
                    this.$emit("editEmployee", {emp : this.emp , mode: mode});
                }).catch(error => {
                  console.log(error);
                }) 
        },

        /**
         * Hàm xóa/ nhân bản/ ngừng sử dụng
         * author: LTQN (15/9/2022)
         */
        tool(e){
            try {
                if(e.tool === resource.toolDropList.Delete){ //nếu là xóa
                    this.$emit('showPopUp', e.emp);
                } 
                if(e.tool === resource.toolDropList.Duplicate){ // nếu là nhân bản
                    this.editEmployee(e.emp.employeeID, enumeration.Mode.ADD);
                }

                this.isShowDataDropList = [];
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
            } catch (error) {
                console.log(error);
            }

        },

          /**
         * Lấy vị trí con chuột
         * author: LTQN(2/10/2022)
         * @param {*} event 
         */
         getPosition(event){
            this.posX = event.pageX;
            this.posY = 220;
        },

        /**
         * Hiển thị tooltip
         * author: LTQN(2/10/2022)
         * @param {string} content : nội dung
         */
        showTooltip(content){
            this.$emit('showTooltip', {contentTooltip: content, positionX: this.posX, positionY: this.posY});
           
        },

        /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip(){
            this.$emit('hideTooltip');
        },


        /**
         * khi tick nút chọn all
         * author: LTQN(5/10/2022)
         */
        clickAll(){
            if(this.checkedAll.length == 0){ //nếu chưa chọn
                this.checkedAll.push(this.pageNumber);
                this.employees.map(employee => {
                    this.checkedEmployees.push(employee.employeeID);
                })
                this.$emit("deleteMultiple", {checkedEmp: this.checkedEmployees, checkedAll: this.checkedAll});
                this.lengthCheckedOfPage = this.pageSize;
            }
            else{
                this.lengthCheckedOfPage = 0;
                this.checkedEmployees = [];
                this.checkedAll = [];
            }

        },

        closeDropList(){
            this.isShowDataDropList = [];
        },
        asc(e){
            let me = this;
            this.isLoadTable = true;
            let temp = this.urlTemp;
            this.urlTemp = this.urlTemp + "&sort=" + e+" ASC";
            getUserAsync(this.urlTemp, me).then(data =>{
                this.isLoadTable = false;
                this.$emit('dataFromTable', {TotalRecord: data.totalCount, TotalPage: Math.ceil(data.totalCount/this.pageSize)});
                this.employees = data.data;
                this.urlTemp= temp;


            })
        },
        desc(e){
            let me = this;
            this.isLoadTable = true;
            let temp = this.urlTemp;
            this.urlTemp = this.urlTemp + "&sort=" + e+" DESC";
            getUserAsync(this.urlTemp, me).then(data =>{
                this.isLoadTable = false;
                this.$emit('dataFromTable', {TotalRecord: data.totalCount, TotalPage: Math.ceil(data.totalCount/this.pageSize)});
                this.employees = data.data;
                this.urlTemp= temp;
            })
        }
    },
    data() {
        return {
            employees: [],
            chooseTr: null,
            emp: {},
            style: {
                center: true,
                right: true
            },
            isShowDataDropList: [],
            dataDropList: resource.dataDropList,
            topDropList: null,
            isLoadTable: false,
            modeEdit: enumeration.Mode.EDIT,
            hiddenDataOfDropList: [],
            posX: 0,
            posY: 0,
            checkedEmployees: [],
            checkedAll: [], //chọn tất cả
            lengthCheckedOfPage: 0,
            isShowWork: false,
            urlTemp: ''
        };
    },
    watch: {
        checkedEmployees(newvalue, oldValue){ 
            if(newvalue.length > oldValue.length)
                this.lengthCheckedOfPage++;
            if(newvalue.length < oldValue.length)
                this.lengthCheckedOfPage--;
            // if(oldValue.length != 0)
                // this.lengthCheckedOfPage += (newvalue.length - oldValue.length);
            if(this.lengthCheckedOfPage != this.employees.length){
                if(this.checkedAll.includes(this.pageNumber)){
                    for(let checked in this.checkedAll){
                        if( this.checkedAll[checked] === this.pageNumber){
                            this.checkedAll.splice(checked,1);
                            break;
                        }
                    }
                }
                 
            }else{
                if(oldValue.length != 0)
                    this.checkedAll.push(this.pageNumber);
            }
            this.$emit("deleteMultiple", {checkedEmp:newvalue,  checkedAll: this.checkedAll});
        },

      
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




