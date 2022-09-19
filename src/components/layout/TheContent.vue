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
                    <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên" v-model="search" @keyup="searchEmployee">
                    <div class="icon-search"></div>
                </div>
                <div class="refresh" @click="reload"></div>
            </div>
            <TheTable @editEmployee ='editEmployee' 
            @getEmployees ='getEmployees' @showPopUp ='showPopUp' 
            :key ='keyReloadTable' @loading ='loading'
            :searchEmployee = 'search'/>

            <ThePagination :TotalEmployee="employees.length" :key='keyReloadPagination' :pagination='pagination'/>
        </div>
        
    </div>
    <TheForm  v-if="showForm" 
    @closeForm='closeForm' :employee="emp" :mode='formMode' 
    @reload='reload'
    @reLoadForm = 'reLoadForm'
    @warningEmpty = 'warningEmpty'
    @openToast = 'openToast'
    :key = 'keyForm'/>
    <MPopup v-if="isShowFopup" :deleteEmployee='deleteEmployee' @closePopup='closePopup' @deleteEmp='deleteEmp'/>
    <MPopupWarning v-show='isShowWarning' @closeWarning='closeWarning' :text='textWarning'/>
    <!-- <MToastMessage v-show="isShowToast" @closeToast='closeToast'/> -->
    <transition name="toast-message">
        <MToastMessage v-show='isShowToast' :content = 'contentOfToastMessage' 
        @closeToastMessage='closeToastMessage' :class='{"toast-success": isError == false, "toast-error": isError}'/>
    </transition>
    <div class="loading" v-show="isLoading">
        <div class="container"></div>
        <img class="img-load" src="../../assets/loading.svg" loading="lazy" alt="loading">
    </div>

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
import Enumeration from '../../common/Enumeration';
// import MToastMessage from '../base/MToastMessage.vue';


export default {
    components: { ThePagination, MButton, TheForm, MPopup, TheTable, MPopupWarning, MToastMessage },
    created(){
        fetch(`${Resource.Url}Employees`)
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
            this.deleteEmployee=e;
            this.isShowFopup = true;
        },
        /**
         * Đưa dữ liệu lên form để sửa
         * author: LTQN(10/9/2022)
         */
        editEmployee(e){
            try {
                this.emp = e;
                this.formMode = Enumeration.Mode.EDIT;
                this.showForm = true;
            } catch (error) {
                console.log(error)
            }
            
        },
        /**
         * Ham mở form khi thêm mới
         * author: LTQN(10/9/2022)
         */
        addEmployee(){
            try {
                this.emp = {};
                this.showForm = true;
                this.formMode = Enumeration.Mode.ADD;
            } catch (error) {
                console.log(error)
            }

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
            let url = `${Resource.Url}Employees/` + e;
            fetch(url, {method: 'DELETE'})
            .then(res => res.json())
            .then(res => {
                //load lại trang
                this.reload();
                //hiển thị toast
                this.openToast(Resource.ToastMessage.success);
                console.log(res);
            }).catch((error) => {
               console.error('Error:', error);
               this.openToast(Resource.ToastMessage.error);
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
         * Hàm hiện cảnh báo mã nhân viên / tên/ đơn vị trống
         * author: LTQN(12/9/2022)
         */
         warningEmpty(msg){
            try {
                this.isShowWarning = true;
                switch(msg){
                    case Resource.popupWarning.EmptyCode.name:
                        this.textWarning = Resource.popupWarning.EmptyCode.content;
                    break;
                    case Resource.popupWarning.EmptyName.name:
                        this.textWarning = Resource.popupWarning.EmptyName.content;
                    break;
                    case Resource.popupWarning.EmptyDepartment.name:
                        this.textWarning = Resource.popupWarning.EmptyDepartment.content;
                    break;
                    default:
                        break;
                }
            } catch (error) {
                console.log(error);
            }
           
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
            try {
                let me=this;
                this.contentOfToastMessage = msg;
                this.isShowToast = true;
                if(msg === Resource.ToastMessage.success)
                    this.isError = false;
                else this.isError = true;
                setTimeout(function(){
                    me.isShowToast = false;
                }, 2000);
            } catch (error) {
                console.log(error)
            }
            
        },
        
        /**
         * Hàm ẩn hiên loading
         * author: LTQN (17/9/2022)
         * @param {boolean} msg : true: hiện; false: ẩn
         */
        loading(msg){
            this.isLoading = msg;
        },

        /**
         * Hàm tìm kiếm theo họ tên
         * author: LTQN(19/9/2022)
         * @param {*} event sk bàn phím
         */
        searchEmployee(event){
            if(event.keyCode == Enumeration.keyCode.ENTER){
                this.reload();
            }
        },

        /**
         * Hàm reload lại form khi ấn cất và thêm
         * author: LTQN(19/9/2022)
         */
        reLoadForm(){
            try {
                this.formMode = Enumeration.Mode.ADD;
                this.emp = {};
                this.keyForm = Math.floor(Math.random()*90000);
            } catch (error) {
                console.log(error)
            }
        }

    },
    data() {
        return{
            employees: [],
            empLength: null,
            showForm: false,
            emp: {}, //khi sửa
            isShowFopup: false,
            deleteEmployee: '',
            formMode: null,
            style: {
                center: true,
                right: true
            },
            keyReloadTable: null,
            keyReloadPagination: null,
            keyForm: null,
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
            },
            isLoading: false,
            search: '', //nội dung tìm kiếm
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
