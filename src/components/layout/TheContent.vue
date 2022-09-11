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
            <TheTable @editEmployee='editEmployee' @getEmployees='getEmployees' @showPopUp='showPopUp' :key='keyReload'/>

            <ThePagination :TotalEmployee="employees.length"/>
        </div>
        
    </div>
    <TheForm  v-if="showForm" @closeForm='closeForm' :employee="emp" :mode='formMode' @reload='reload'/>
    <MPopup v-if="isShowFopup" :id='deleteEmployeeID' @closePopup='closePopup' @deleteEmp='deleteEmp'/>
 
</template>

<script>
import ThePagination from './ThePagination.vue';
import MButton from '../base/MButton.vue';
import TheForm from '../view/employee/TheForm.vue';
import MPopup from '../base/MPopup.vue';
import TheTable from '../view/employee/TheTable.vue';


export default {
    components: { ThePagination, MButton, TheForm, MPopup, TheTable },
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
                let randomKey = Math.floor(Math.random()*10000);
                this.keyReload = randomKey;
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
            this.keyReload = randomKey;
        },

        getEmployees(e){
            Object.assign(this.employees, e);
        }
    },
    data() {
        return{
            employees: [],
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
            keyReload: null
        }
    },
    
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/content.css);
</style>
