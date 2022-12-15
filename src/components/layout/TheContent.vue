<template>
    <div class="content">
        <div class="content-header">
            <div class="title">Nhân viên</div>
            <div class="tool-bar">
                <MButton class="add-employee" :text= "'Thêm mới nhân viên'"  @click="addEmployee"/>
            </div>
        </div>
        <div class="container-grid">
            <div class="tool-filter">
                <div class="tool-delete">
                    <MOvalButton :text="'Thực hiện hàng loạt'" :type="'Xóa'" :listID="listIdDelete" @deleteIDs='showPopUpDeleteMultiple' :isShowDelete='showDeleteMultip'/>
                    <div class="container-icon">
                        <div class="icon-filter" @click="this.isCancelFilter =! this.isCancelFilter;"></div>              
                        <div class="container-filter" v-show="isCancelFilter">
                            <div style="width: 270px; display:flex;">
                            <MFilter :url="Url + 'Departments'" @objectItemCombobox="filterDepartment"
                                        :textName="'department'" :clear="clearDep"
                                        :placeholder="'Lọc theo đơn vị'"/>
                                <div class="icon-filter" @click="filterDep"></div>    
                                <div class="cancel-filter" v-show="isCancelFilterDep">x</div> 
                            </div>
                            <div style="width: 270px; display:flex;">
                                <MFilter :url="Url + 'LoPositions'" :textName="'position'" @objectItemCombobox="filterPosition"
                                :placeholder="'Lọc theo Chức vụ'" :clear="clearPos"/>
                                <div class="icon-filter" @click="filterPos"></div>    
                                <div class="cancel-filter pos" v-show="isCancelFilterPos">x</div> 
                            </div>
                        </div>
                    </div>
                    
                   
                </div>
                <div class="filter">
                    <div class="search input-icon"> 
                    <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên" v-model="search" @keyup="searchEmployee">
                    <div class="icon-search" @click="searchEmployee"></div>
                    </div>
                    <div class="refresh" @click="reload"></div>
                    <div class="export" @click="exportEmployee">
                    </div>
                    <a :href="href" class="link-export" ref="export" download = 'Danh sách nhân viên'></a>
                   
            </div>
            </div>
            
           

            <TheTable @editEmployee ='editEmployee' 
             @showPopUp ='showPopUp' :conditionPos="conditionPos" :conditionDep="conditionDep"
            :key ='keyReloadTable'
            :searchEmployee = 'search'
            :pageSize = 'pageSize' :pageNumber='pageNumber'
            @dataFromTable = 'dataFromTable'
            @editEmployeeStatus = 'editEmployeeStatus'
            @showTooltip='showTooltip' @hideTooltip='hideTooltip'
            @deleteMultiple='getIdDelete' @openToast='openToast' 
            :listIdDelete='listIdDelete' :listCheckAllOfPage='listCheckAllOfPage'/>

            <ThePagination :totalRecord="totalRecord" :key='keyReloadPagination' :totalPage='totalPage'
            @changePage='changePage' @recordOfPage='recordOfPage' :pageBeforeLoad='pageNumber'/>
        </div>
        
    </div>
    <TheForm  v-if="showForm"  :focus = 'focus' :title="titleForm"
    @closeForm='closeForm' :employee="emp" :mode='formMode' 
    @reload='reload' @reLoadForm = 'reLoadForm'
    @warningEmpty = 'warningEmpty' @warningEmail = 'warningEmail' @warningError = "warningError" @warningDate="warningDate"
    @openToast = 'openToast' :saveForm="isSave"
    :key = 'keyForm' @showTooltip='showTooltip' @hideTooltip='hideTooltip'
    @openPopUpChange= 'openPopUpChange'/>
    <MPopup v-if="isShowFopup" :deleteEmployee='deleteEmployee' @closePopup='closePopup' @deleteEmp='deleteEmp' :type='typePopup'
    @closeFrom='closeForm' @saveForm='saveForm' @deleteEmpMultiple='deleteIDs' :textPopUp='textPopUp'/>
    <MPopupWarning v-if='isShowWarning' @closeWarning='closeWarning' :text='textWarning' :type='typePopup'/>

    <transition name="toast-message">
        <MToastMessage v-show='isShowToast' :content = 'contentOfToastMessage' 
        @closeToastMessage='closeToastMessage' :type='typeToast'/>
    </transition>

    <MTooltip :content="contentToolTip" v-show="isShowTooltip" :style="{'top': `${posYTooltip}px`, 'left': `${posXTooltip}px`}"/>

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
import resource from '../../common/resource'
import enumeration from '../../common/enumeration';
import MTooltip from '../base/MTooltip.vue';
import MOvalButton from '../base/MOvalButton.vue';
import MFilter from '../base/MFilter.vue';
// import MToastMessage from '../base/MToastMessage.vue';


export default {
    components: { ThePagination, MButton, TheForm, MPopup, TheTable, MPopupWarning, MToastMessage, MTooltip, MOvalButton, MFilter },
    props:{
        isToastMsgInfo: Boolean
    },
    mounted(){
        document.addEventListener("keydown", this.eventKeycode);

    },
    updated(){
    },
    unmounted(){
        document.removeEventListener("keydown", this.eventKeycode);
    },
    methods: {
         /**
         * Ấn keycode
         * author: LTQN(6/10/2022)
         */
         eventKeycode(event){
            if (event.ctrlKey && event.key === 'a') {
                event.preventDefault();
                this.addEmployee();
            }

        },
        /**
         * Ham đóng form
         * author: LTQN(9/9/2022)
         */
        closeForm(){
            this.showForm = false;
            this.isShowTooltip = false;
        },
        /**
         * Hiện popup xóa
         * author: LTQN(10/9/2022)
         */
        showPopUp(e){
            this.deleteEmployee=e;
            this.typePopup = resource.PopUpType.Warning;
            this.textPopUp = `Bạn có thực sự muốn xóa nhân viên <${this.deleteEmployee.employeeCode}> không?`
            this.isShowFopup = true;
        },

        /**
         * Hiện popup thông báo thay đổi dữ liệu
         * author: LTQN(6/10/2022)
         */
        openPopUpChange(){
            this.deleteEmployee={};
            this.typePopup = resource.PopUpType.Question;
            this.textPopUp = 'Dữ liệu bị thay đổi. Bạn có muốn cất không?';
            this.isShowFopup = true;
        },
        /**
         * Đưa dữ liệu lên form để sửa/nhân bản
         * author: LTQN(10/9/2022)
         */
        editEmployee(e){
            try {
                this.emp = e.emp;
                this.formMode = e.mode;
                this.showForm = true;
                this.titleForm = 'Sửa thông tin nhân viên'
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
                this.formMode = enumeration.Mode.ADD;
                this.titleForm = 'Thêm thông tin nhân viên';
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
            let url = `${process.env.VUE_APP_URL}Employees/` + e;
            fetch(url, {method: 'DELETE'})
            .then(res => res.json())
            .then(res => {
                //load lại trang
                this.reload();
                //hiển thị toast
                this.openToast(resource.ToastMessageType.success, resource.ToastMessage.success);
                console.log(res);
            }).catch((error) => {
               console.error('Error:', error);
               this.openToast(resource.ToastMessageType.error, resource.ToastMessage.error);
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
         * Hàm đóng cảnh báo
         * author: LTQN(10/9/2022)
         */
        closeWarning(){
            this.isShowWarning = false;
            this.focus =! this.focus;
        },

        /**
         * Hàm hiện cảnh báo mã nhân viên / tên/ đơn vị trống
         * author: LTQN(12/9/2022)
         */
         warningEmpty(msg){
            try {
                this.isShowWarning = true;
                this.typePopup = resource.PopUpType.Info;
                switch(msg){
                    case resource.popupWarning.EmptyCode.name:
                        this.textWarning = resource.popupWarning.EmptyCode.content;
                    break;
                    case resource.popupWarning.EmptyName.name:
                        this.textWarning = resource.popupWarning.EmptyName.content;
                    break;
                    case resource.popupWarning.EmptyDepartment.name:
                        this.textWarning = resource.popupWarning.EmptyDepartment.content;
                    break;
                }


            } catch (error) {
                console.log(error);
            }
           
        },
        /**
         * Validate email
         * author: LTQN(23/9/2022)
         */
        warningEmail(){
            this.textWarning = 'Email không đúng định dạng.';
            this.typePopup = resource.PopUpType.Warning;
            this.isShowWarning = true;
        },

        /**
         * thông báo bị lỗi từ db
         * author: LTQN(25/9/2022)
         * @param {string} text : nội dung cảnh báo
         */
         warningError(text){
            this.textWarning = text;
            this.typePopup = resource.PopUpType.Warning;
            this.isShowWarning = true;
        },

        /**
         * Validate ngày tháng
         * author: LTQN(3/10/2022)
         */
        warningDate(){
            this.textWarning = 'Ngày tháng không lớn hơn hiện tại.';
            this.typePopup = resource.PopUpType.Info;
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
        openToast(type, msg){ 
            try {
                const me=this;
                this.contentOfToastMessage = msg;
                this.isShowToast = true;

                let typeOfToasts = resource.ToastMessageType
                for(let typeToast in typeOfToasts){
                    if(typeOfToasts[typeToast] === type)
                        this.typeToast = typeOfToasts[typeToast];
                }

                setTimeout(function(){
                    me.isShowToast = false;
                }, 2000);
            } catch (error) {
                console.log(error)
            }
            
        },

        /**
         * Hàm sửa status: ngưng sử dụng/sử dụng
         * author: LTQN(29/9/2022)
         * @param {Employee} emp : đối tượng cần sửa
         */
        async editEmployeeStatus(emp){
            console.log(emp);
            let url = `${process.env.VUE_APP_URL}Employees/` + emp.employeeID;
            let response = await fetch(url, {
                method: 'PUT',
                headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                body: JSON.stringify(emp),
            }) 
            
            if(response.ok){
                this.reload();
                this.openToast(resource.ToastMessageType.success, resource.ToastMessage.success)
            }
            else{ //lỗi khác: >=400 && <600
                let data = await response.json();
                console.log(data);
                this.openToast(resource.ToastMessageType.error, resource.ToastMessage.error);
            }

        },

        /**
         * Hàm tìm kiếm theo họ tên
         * author: LTQN(19/9/2022)
         * @param {*} event sk bàn phím
         */
        searchEmployee(event){
            if(event.keyCode === enumeration.keyCode.ENTER || event.type === resource.event.Click){
                this.pageNumber = 1;
                this.reload();
            }
        },

        /**
         * Hàm reload lại form khi ấn cất và thêm
         * author: LTQN(19/9/2022)
         */
        reLoadForm(){
            try {
                this.formMode = enumeration.Mode.ADD;
                this.emp = {};
                this.titleForm = 'Thêm thông tin nhân viên';
                this.keyForm = Math.floor(Math.random()*90000);
            } catch (error) {
                console.log(error)
            }
        },


        /**
         * Hàm load lại table khi chọn trnag mới
         * author: LTQN(20/9/2022)
         * @param {int} page : Trang được chọn
         */
        changePage(page){
            this.pageNumber = page;
            this.reload();
        },
        
        /**
         * Hàm lấy thông tin từ API truyền vào pagination
         * author: LTQN(20/9/2022)
         * @param {Object} data {totalRecord: Tổng số bản ghi, ttalPage: Tổng trang}
         */
        dataFromTable(data){
            this.totalRecord = data.TotalRecord;
            this.totalPage = data.TotalPage;

            if(sessionStorage.setItem('pageRecord', JSON.stringify({record: this.totalRecord, page: this.totalPage})));
        },

        /**
         * Hàm chọn số bản ghi trong 1 trang
         * author: LTQN(20/9/2022)
         * @param {int} numRecord : Sô bản ghi trong 1 trang
         */
        recordOfPage(numRecord){
            this.pageSize = numRecord;
            this.pageNumber = 1;
            this.reload();
        },

        /**
         * Hàm xuất khẩu
         * author: LTQN(2/10/2022)
         */
        exportEmployee(){
            let url = `${process.env.VUE_APP_URL}Employees/data-export`;
            this.isLoading = true;
            fetch(url).then(response => response.blob())
            .then(blob => {
                const reader = new FileReader;
                
                reader.addEventListener('load', () => {
                    this.href = reader.result;
                    this.isLoading = false;
                });
                reader.readAsDataURL(blob);

            }).catch(error => {
                console.log(error);
            })
            
        },

        /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip(){
            this.isShowTooltip = false;
            this.posXTooltip = 0;
            this.posYTooltip = 0;
        },

        /**
         * Hiển thị tooltip
         * author: LTQN(2/10/2022)
         * @param {object} e : nội dung/ vị trí
         */
        showTooltip(e){
            this.contentToolTip = e.contentTooltip;
            this.posXTooltip = e.positionX;
            this.posYTooltip = e.positionY;
            this.isShowTooltip = true;
        },
         /**
          * Lấy danh sách emp được chọn
            * author LTQN(6/10/2022)
          * @param {object} emps : {checkedEmp: danh sách emp chọn, checkedAll: danh sách trang mà trang đó chọn tất cả}
          */
        getIdDelete(emps){
            this.listIdDelete = [];
            this.listCheckAllOfPage = emps.checkedAll;

            emps.checkedEmp.map((emp) => {
                this.listIdDelete.push(emp);
            })
            //hiển thị nút xóa
            if(emps.checkedEmp.length > 1){
                this.showDeleteMultip = true;
            }
            else{
                this.showDeleteMultip = false;                          
            }  
    
        },

        /**
         * XÓa nhiều emp được chọn
         * author LTQN(6/10/2022)
         */
        async deleteIDs(){
            try {
                let url = `${process.env.VUE_APP_URL}Employees/deletion-requests`;
                // let ids = this.listIdDelete.join(",");
                let response = await fetch(url,{
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    method: "POST",
                    body:  JSON.stringify(this.listIdDelete)
                    })
                // thực hiện thành công
                if(response.ok){
                    this.reload();
                    //hiển thị toast
                    this.openToast(resource.ToastMessageType.success, resource.ToastMessage.success);        
                } //thực hiện thất bại
                else{ //lỗi khác: >=400 && <600   
                    this.openToast(resource.ToastMessageType.error, resource.ToastMessage.error);
                }
                this.showDeleteMultip = false; 
                this.isShowFopup = false;
            } catch (error) {
                console.log(error)
            }
           
        },

        /**
         * Thực hiện save form từ popup
         * author: LTQN(6/10/2022)
         */
        saveForm(){
            this.isSave =! this.isSave;
        },

        /**
         * Hiển thị cảnh báo xóa hàng loạt
         * author: LTQN(7/10/2022)
         */
        showPopUpDeleteMultiple(){
            this.typePopup = resource.PopUpType.Warning;
            this.deleteEmployee={};
            this.isShowFopup = true;
            this.textPopUp = 'Bạn có thực sự muốn xóa những nhân viên đã chọn không?'
        },

        filterDepartment(e){
            this.conditionDep = e.departmentName;
        },

        filterPosition(e){
            this.conditionPos = e.positionName;
        },
        filterDep(){
            if(this.isCancelFilterDep){
                this.conditionDep = '';
                this.clearDep =! this.clearDep;
            }
            this.reload();
            this.isCancelFilterDep =! this.isCancelFilterDep;
        },
        filterPos(){
            if(this.isCancelFilterPos){
                this.conditionPos = '';
                this.clearPos =! this.clearPos;

            }
            this.reload();
            this.isCancelFilterPos =! this.isCancelFilterPos;
        }

    },
    data() {
        return{
            employees: [],
            empLength: null,
            showForm: false,
            titleForm: '',
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
            textPopUp: '', //nội dung hiện pop up xóa
            textWarning: '', //nội dung hiện pop up cảnh báo
            typePopup: '', //icon tương ứng với các pop up
            numOfButton: false, //số lượng button trong popup, false: 3 nút, true: 2 nust
            isShowToast: false, //hiện ẩn toast message
            contentOfToastMessage: '', //nội dung toast message
            typeToast: '', //loại toast
            isError: '', //Lỗi hay không để hiển thị toast
            totalPage: null, // tổng số trang
            totalRecord: null, //tổng số bản ghi
            isLoading: false,
            search: '', //nội dung tìm kiếm
            pageSize: 20,
            pageNumber: 1,
            href: null, //link export
            contentToolTip: '',
            posXTooltip: null,
            posYTooltip: null,
            isShowTooltip: false,
            focus: null,
            listIdDelete: [], //danh sách id muốn xóa
            listCheckAllOfPage: [], //danh sách trang chọn tất cả
            showDeleteMultip: false, //hiển thị nút xóa nhiều
            isSave: false, //pop up gửi trạng thái muốn lưu form
            Url: process.env.VUE_APP_URL,
            isCancelFilterDep: false,
            isCancelFilterPos: false,
            isCancelFilter: false,
            conditionPos: '',
            conditionDep: '',
            clearDep: false,
            clearPos: false
        }
    },


    watch:{
        href(){
            setTimeout(() => {{
                this.$refs.export.click(); 
            }},100)

        },

        isToastMsgInfo(){
            this.openToast(resource.ToastMessageType.info, resource.ToastMessage.info);
        }
        
    }
    
}


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/content.css);
</style>
