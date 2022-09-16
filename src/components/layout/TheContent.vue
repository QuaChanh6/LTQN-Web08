<template>
    <div class="content">
        <div class="content-header">
            <div class="title">Nhân viên</div>
            <div class="tool-bar">
                <MButton class="add-employee" :text= "'Thêm mới nhân viên'" @click="addEmployee()"/>
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
            <TheTable @editEmployee='editEmployee' @getEmployees='getEmployees' @showPopUp='showPopUp' :key='keyReloadTable'/>

            <ThePagination :TotalEmployee="employees.length" :key='keyReloadPagination' :pagination='pagination'/>
        </div>
        
    </div>
    <TheForm  v-if="showForm" 
    @closeForm='closeForm' :employee="emp" :mode='formMode' 
    @reload='reload'
    @warningEmptyCode = 'warningEmptyCode'
    @warningEmptyName = 'warningEmptyName'
    @warningEmptyDepartment = 'warningEmptyDepartment'
    @openToast = 'openToast'/>
    <MPopup v-if="isShowFopup" :id='deleteEmployeeID' 
    @closePopup='closePopup' @deleteEmp='deleteEmp' :content= "'Bạn có thực sự muốn xóa nhân viên không'"/>
    <MPopupWarning v-show='isShowWarning' @closeWarning='closeWarning' :text='textWarning'/>
    <!-- <MToastMessage v-show="isShowToast" @closeToast='closeToast'/> -->
    <transition name="toast-message">
        <MToastMessage v-show='isShowToast' :content = 'contentOfToastMessage' 
        @closeToastMessage='closeToastMessage' :class='{"toast-success": isError = false, "toast-error": isError}'/>
    </transition>
  

</template>

<script>
import ThePagination from './ThePagination.vue';
import MButton from '../base/MButton.vue';
import TheForm from '../view/employee/TheForm.vue';
import MPopup from '../base/MPopup.vue';
import TheTable from '../view/employee/TheTable.vue';
import MPopupWarning from '../base/MPopupWarning.vue';
import MToastMessage from '../base/MToastMessage.vue';
import Resource from '../../common/Resource';
// import Enumeration from '../../common/Enumeration';
// import MToastMessage from '../base/MToastMessage.vue';


export default {
    components: { ThePagination, MButton, TheForm, MPopup, TheTable, MPopupWarning, MToastMessage },
    created(){
        fetch("https://cukcuk.manhnv.net/api/v1/Employees")
        .then(res => res.json())
        .then(res => {
            this.employees=res;
        })
    },
    methods: {
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
        editEmployee(e){
            this.emp = e;
            this.formMode = this.modeOfForm.EDIT;
            this.showForm = true;
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
                this.keyReloadTable = Math.floor(Math.random()*10000);
                this.isShowToast = true;
                console.log(res);
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
            this.keyReloadTable = Math.floor(Math.random()*90000);
        },
        /**
         * Hàm lấy thông tin tất cả employee từ table
         * author: LTQN(10/9/2022)
         */
        getEmployees(e){
            this.employees = e;
            this.empLength = e.length;
        },
        /**
         * Hàm đóng cảnh báo
         * author: LTQN(10/9/2022)
         */
        closeWarning(){
            this.isShowWarning = false;
        },
        /**
         * Hàm hiện cảnh báo mã nhân viên trống
         * author: LTQN(11/9/2022)
         */
        warningEmptyCode(){
            this.textWarning = 'Mã nhân viên không được để trống.';
            this.isShowWarning = true;
        },

        /**
         * Hàm hiện cảnh báo mã nhân viên trống
         * author: LTQN(12/9/2022)
         */
         warningEmptyName(){
            this.textWarning = 'Tên không được để trống.';
            this.isShowWarning = true;
        },
        /**
         * Hàm hiện cảnh báo đơn vị trống
         * author: LTQN(12/9/2022)
         */
        warningEmptyDepartment(){
            this.textWarning = 'Đơn vị không được để trống.';
            this.isShowWarning = true;
        },
        /**
         * Hàm đóng toastmessage
         * author: LTQN(15/9/2022)
         */
         closeToastMessage(){
            this.isShowToast = false;
        },
        /**
         * Hàm mở toastmessage
         * author: LTQN(15/9/2022)
         */
        openToast(msg){ 
            let me=this;
            me.contentOfToastMessage = msg;
            me.isShowToast = true;
            if(msg == Resource.ToastMessage.success)
                me.isError = false;
            else me.isError = true;
            setTimeout(function(){
                me.isShowToast = false;
            }, 2000);
        }
    },
    data() {
        return{
            employees: [],
            empLength: null,
            showForm: false,
            emp: {},
            isShowFopup: false,
            deleteEmployeeID: '',
            modeOfForm : {
                ADD: 1,
                EDIT: 2
            },
            formMode: null,
            style: {
                center: true,
                right: true
            },
            keyReloadTable: null,
            keyReloadPagination: null,
            isShowWarning: false, //hiện/ẩn pop up
            textWarning: '', //nội dung hiện pop up cảnh báo
            isShowToast: false, //hiện ẩn toast message
            contentOfToastMessage: '', //nội dung toast message
            isError: '', //Lỗi hay không để hiển thị toast
            pagination: {
                first: '1',
                second: '2',
                third: '3',
                last: null,
            }
        }
    },
    watch: {
        empLength(){
            this.keyReloadPagination = Math.floor(Math.random()*100000);
        }
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/content.css);
</style>
