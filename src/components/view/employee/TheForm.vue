
<template>
    <div class="form-container" ref="form">
        <div class="container"></div>
        <div class="form">
        <div class="form-header">
            <div class="icon-form">
                <div class="info-more">
                </div>
                <div class="icon-x" @click="closeForm"></div>
            </div>
            <div class="form-title">
                <div class="title-employee">{{title}}</div>
                <div class="radio">
                    <input class="input-radio" type="radio" name="status" value="0" v-model="pickedStatus">Đang làm việc
                    <input class="input-radio" type="radio" name="status" value="1"  v-model="pickedStatus">Tạm nghỉ
                    <input class="input-radio" type="radio" name="status" value="2"  v-model="pickedStatus">Đang thử việc
 
                </div>
            </div>
        </div>
        
        <div class="form-body">
            <div class="row">
                <div class="col-left">
                    <div class="employee-code"  :class="{'hoverEmptyCode': emptyCode}">
                        <label for="">Mã <span class="require"> *</span></label>
                        <input type="text" class="input" placeholder="Mã nhân viên" 
                        @keyup = 'validateFocusCode'
                        @focus = 'validateFocusCode'
                        :ref="'Code'" v-model='emp.employeeCode' :class="{'border-red': emptyCode}"/>
                        <div class="not-empty emptyCode">Không được để trống. </div>
                    </div>
                    <div class="name" :class="{'hoverEmptyName': emptyName}">
                        <label for="">Tên <span class="require"> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên"
                        @focus = 'validateFocusName' :ref="'Name'"
                        @keyup="validateFocusName"  v-model='emp.employeeName' :class="{'border-red': emptyName}">
                        <div class="not-empty emptyName">Tên không được để trống. </div>
                    </div>
                </div>
                <div class="col-right">
                    <div class="date-of-birth">
                        <label for="">Ngày sinh</label>
                        <!-- <input type="date" class="input" v-model="emp.dateOfBirth" name="date"> -->
                        <Datepicker class="datepicker" :enableTimePicker="false" 
                        v-model="emp.dateOfBirth" placeholder="DD-MM-YYYY" textInput
                        format = 'dd-MM-yyyy' locale="vi" autoApply
                        :dayNames="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"></Datepicker>
                    </div>
                    <div class="gender">
                        <label for="">Giới tính</label>
                        <div class="radio">
                            <input class="input-radio" type="radio" name="gender" value="0" v-model="picked">Nam
                            <input class="input-radio" type="radio" name="gender" value="1"  v-model="picked">Nữ
                            <input class="input-radio" type="radio" name="gender" value="2"  v-model="picked">Khác
                        </div>
                    </div>
                </div>
                
                
            </div>
           
            <div class="row">
                <div class="col-left">
                    <div class="identify-number"  @mousemove="getPosition" @mouseleave="hideTooltip">
                        <label for="" @mouseover="showTooltip('Số chứng minh nhân dân')">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.identityNumber">
                  
                    </div>
                </div>
                <div class="col-right">
                    <div class="address">
                        <label for="">Địa chỉ</label>
                        <input type="text" class="input" placeholder="Địa chỉ liên lạc" v-model="emp.address">
                    </div>
                </div>
            
            </div>
            <div class="row">
               
            </div>
            <div class="row">
                <div class="phone-number">
                    <label for="">Điện thoại di động</label>
                    <input type="text" class="input" placeholder="Số điện thoại di động" v-model="emp.phoneNumber">
                </div>
                <div class="email">
                    <label for="">Email</label>
                    <input type="text" class="input" placeholder="Email" v-model="emp.email" ref="Email">
                </div>
            </div>
            <div class="container-labelWork">
                <label class="labelWork" @click="isShowEditWork =! isShowEditWork"><div class="icon-pen"></div>Sửa thông tin vị trí công việc</label>
                <label class="labelWork" v-if="mode == 2" @click="showWork(emp.employeeCode)"><div class="icon-view"></div>Xem thông tin lịch sử công việc</label>
            </div>
           <!-- <div @click="showFile =! showFile"><label>Bằng cấp đính kèm</label></div> -->

            <div class="row" v-if="isShowEditWork">
                <div class="col-left">
                    <div class="department">
                        <label for="">Đơn vị <span class="require"> *</span></label>
                        <MCombobox :url="Url + 'Departments'" 
                        :textName="'department'" :focus="focusDepartment"
                        @objectItemCombobox='objectItemCombobox' :emptyCombobox="emptyDepartment"
                        :valueRender='dataCombobox' @comboboxFocus="comboboxFocus"
                         :placeholder="'Đơn vị'"/>
            
                    </div>
                </div>
                <div class="col-right">
                   <div class="position">
                        <label for="">Chức vụ</label>
                        <MCombobox :url="Url + 'Positions'" @objectItemCombobox='objectItemComboboxPosition'
                        :textName="'position'" :valueRender='dataComboboxPosition'
                         :placeholder="'Chức vụ'"/>
                   </div>
                   
                </div>
                
            
            </div>
        </div>
        <div class="form-footer">
            <div class="cancel">
                <MButton class="btn-cancel" :text="'Hủy'" @click="sendMessageClose"/>
            </div>
            <div class="save">
                <MButton class="btn-save" :text="'Cất'" @click="save(enumeration.SaveForm.Save)"/>
                <MButton class="btn-save-and-add" :text="'Cất và thêm'" @click="save(enumeration.SaveForm.SaveAndAdd)"/>
            </div>
        </div>
    </div>

    </div>
    <TheTableHistoryWorkVue v-if="isShowWork" :employeeCode = "employeeCode" @closeForm="closeFormWork"/>
    <TheAttachFileVue v-if="showFile" @closeFile="closeFile" :employeeCode="employee.employeeCode"></TheAttachFileVue>
</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
import MCombobox from '@/components/base/MCombobox.vue';
import format from '@/common/formatData';
import enumeration from '@/common/enumeration';
import resource from '@/common/resource';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import TheTableHistoryWorkVue from '../historyWork/TheTableHistoryWork.vue';
import TheAttachFileVue from './TheAttachFile.vue';



 
    /**
     * Hàm validate
     * author: LTQN(14/9/2022)
     * @param {*} form: con trỏ vue
     * @param {object} objectEmpty {emptyCode, emptyName, emptyDepartment} 

     * 
     */
    function validate(pointer, objectEmpty){
        for(let key in objectEmpty){
            if(objectEmpty[key]){
                // form.$emit(`warningEmpty${key}`);
                pointer.form.$emit('warningEmpty', key);
                return false;
            }
        }
        let validateEmail = format.Email(pointer.email);
        pointer.form.formatEmail = true;
        if(!validateEmail){
            pointer.form.$emit('warningEmail');
            pointer.form.formatEmail = false;
            return false;
        }

        let dateBirth= format.checkDate(pointer.dateOfBirth, new Date());
        let identityDate = format.checkDate(pointer.identityDate, new Date());
        if(!dateBirth || !identityDate){
            pointer.form.$emit('warningDate');
            return false;
        }
        
        return true;
    }

    /**
     * hàm xử lý kết quả trả về
     * author: LTQN(10/9/2022)
     * @param {object} res: kết quả trả về của API
     * @param {int} pointer: {this, mode}
     */
     async function handleResponse(res, pointer){
        try {
              // thực hiện thành công
            if(res.ok){
                pointer.form.$emit('openToast', resource.ToastMessageType.success, resource.ToastMessage.success);
                pointer.form.$emit('reload');

                //Thành công thì đóng form
                if(pointer.mode === enumeration.SaveForm.SaveAndAdd){ //nếu là cất và thêm
                    pointer.form.$emit('reLoadForm');
                }
                if(pointer.mode === enumeration.SaveForm.Save){ //nếu là cất
                    pointer.form.$emit('closeForm');
                }
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                let data = await res.json();
                pointer.form.$emit('warningError', data); //thông báo cho người dùng
                pointer.form.$emit('openToast', resource.ToastMessageType.error, resource.ToastMessage.error);
            }
    
        } catch (error) {
            console.log(error);
        }
      
    }



  export default {
    components: { MButton, MCombobox, Datepicker, TheTableHistoryWorkVue, TheAttachFileVue},
    props: {
        employee: Object,
        mode: Number,
        focus: Boolean,
        saveForm: Boolean,
        title: String
    },
    created(){
        try {
            this.enumeration = enumeration;
            this.emp = this.employee;
            //lấy mã nhân viên lớn nhất
            if(this.mode === enumeration.Mode.ADD)
                this.getMaxCode();
            
            //xử lý dữ liệu date
            if(!format.checkEmptyData(this.emp.dateOfBirth))
                this.emp.dateOfBirth = this.emp.dateOfBirth.slice(0,10);
            
            //xử lý dữ liệu radio
            if(!format.checkEmptyData(this.emp.gender))
                this.picked = this.emp.gender;
            if(!format.checkEmptyData(this.emp.status))
                this.pickedStatus = this.emp.status;

            //xử lý dữ liệu combobox
            if(!format.checkEmptyData(this.emp.departmentName)){
                this.dataCombobox = {id: this.emp.departmentID, name: this.emp.departmentName};
                this.emptyDepartment = false;
                this.oldValueDep = this.emp.departmentName;
            }
              //xử lý dữ liệu combobox
              if(!format.checkEmptyData(this.emp.positionName)){
                this.dataComboboxPosition = {id: this.emp.positionID, name: this.emp.positionName};
                this.oldValuePos = this.emp.positionName;
            }
            
            Object.assign(this.oldEmp, this.emp);
            

        } catch (error) {
            console.log(error)
        }    
    },
    mounted(){
            //focus vào mã nhân viên
        this.$refs['Code'].focus();
   
        document.addEventListener("keydown", this.eventKeycode);
    },
    updated(){
        try {

            //xử lý khi chọn dữ liệu radio
            this.emp.gender = Number(this.picked);
            if(this.emp.gender === enumeration.gender.Female){
                this.emp.genderName = 'Nữ';
            }
            if(this.emp.gender === enumeration.gender.Male){
                this.emp.genderName = 'Nam';
            }
            if(this.emp.gender === enumeration.gender.Other){
                this.emp.genderName = 'Khác';
            }

            this.emp.status = Number(this.pickedStatus);
            if(this.emp.status === enumeration.status.Work){
                this.emp.statusName = 'Đang làm việc';
            }
            if(this.emp.status === enumeration.status.Pause){
                this.emp.statusName = 'Tạm nghỉ';
            }
            if(this.emp.status === enumeration.status.TryingOut){
                this.emp.statusName = 'Đang thử việc';
            }

            
            
        } catch (error) {
            console.log(error)
        }

    },
    beforeUnmount(){
        document.removeEventListener("keydown", this.eventKeycode);
    },
    methods: {
        closeFile(){
            this.showFile = false;
        },
        closeFormWork(){
            this.isShowWork = false;
        },
        showWork(e){
            this.isShowWork = true;
            this.employeeCode = e;
        },
        /**
         * Ấn keycode
         * author: LTQN(6/10/2022)
         */
         eventKeycode(event){
            if (event.keyCode === enumeration.keyCode.ESC){
                this.closeForm();
            }

            if (event.ctrlKey && event.key === 's') {
                event.preventDefault();
                this.save(enumeration.SaveForm.Save);
            }

            if(event.shiftKey && event.ctrlKey && event.key === 'S'){
                this.save(enumeration.SaveForm.SaveAndAdd);
            }
        },
        /**
         * đóng form
         * author: LTQN(9/9/2022)
         */
        sendMessageClose(){
            this.$emit('closeForm');
        },
        /**
         * hàm lưu dữ liệu
         * author: LTQN(10/9/2022)
         * @param {int} mode: cất/ cất và thêm
         */
        async save(mode){ 
            try {
                 //validate
                let me = {form: this, email: this.emp.email, dateOfBirth: this.emp.dateOfBirth, identityDate: this.emp.identityDate},
                response = {};
                this.checkEmpty();
                let objectEmpty = {
                    'EmptyCode' : this.emptyCode, 'EmptyName' : this.emptyName, 'EmptyDepartment' : this.emptyDepartment
                }
                let valid = validate(me, objectEmpty);
                if(valid){
                    if(this.mode === enumeration.Mode.ADD){ //nếu là thêm
                        response = await fetch(`${this.Url}Employees`,{
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        method: "POST",
                        body: JSON.stringify(this.emp)
                        })
                    }
                    if(this.mode === enumeration.Mode.EDIT){ //nếu là sửa
                        let url= `${this.Url}Employees/` + this.emp.employeeID;
                        response = await fetch(url, {
                        method: 'PUT',
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        body: JSON.stringify(this.emp),
                        }) 
                        this.saveHistoryWork();
                    }
                    //xử lý kết quả trả về
                    me.mode = mode;
                    handleResponse(response, me);

                    
                }
            } catch (error) {
                console.log(error)
            }
           
           
        },

        async saveHistoryWork(){
            let me=this;
            if(this.oldValueDep != this.emp.departmentName || this.oldValuePos != this.emp.positionName){
                let url= `${this.Url}HistoryWork/Work/` + this.emp.employeeCode;
                let response = await fetch(url, {
                        method: 'PUT',
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        body: JSON.stringify(this.emp),
                }) 
                handleResponse(response, me);
                this.oldValueDep = this.emp.departmentName;
                this.oldValuePos = this.emp.positionName;
            }
        },

        /**
         * Hàm kiểm tra tất cả có rỗng không trước khi ấn cất
         * author: LTQN(10/9/2022)
         */
        checkEmpty(){
             // kiểm tra rỗng
            if(format.checkEmptyData(this.emp.departmentName)){
                    this.emptyDepartment = true;
            }else  this.emptyDepartment = false;

            this.validateFocusCode();
            this.validateFocusName();
        },
         /**Hàm lấy giá trị của combobox department
          * author: LTQN(10/9/2022)
          * @param {*} e : giá trị selected trong combobox
          */
        objectItemCombobox(e){
            try {
            
                this.emp.departmentID = e.departmentID;
                this.emp.departmentName = e.departmentName;
                this.emp.departmentCode = e.departmentCode;
                this.empDepartmentName = this.emp.departmentName;
                
                // kiểm tra rỗng
                if(format.checkEmptyData(this.emp.departmentName)){
                    this.emptyDepartment = true;
                }else  this.emptyDepartment = false;
            } catch (error) {
                console.log(error)
            }
 
        },

        objectItemComboboxPosition(e){
            try {
            
            this.emp.positionID = e.positionID;
            this.emp.positionName = e.positionName;
            this.emp.positionCode = e.positionCode;

            } catch (error) {
                console.log(error)
            }

        },
        /**
         * Hàm kiểm tra rỗng khi focus vào input employee Code
         * author: LTQN(10/9/2022)
         *
         */
        validateFocusCode(){
            try {       
                if(format.checkEmptyData(this.emp.employeeCode)){
                this.emptyCode = true;
                }else  this.emptyCode = false;
            } catch (error) {
                console.log(error)
            }

        },
        /**
         * Hàm kiểm tra rỗng khi focus vào input employeeName
         * author: LTQN(10/9/2022)
         *
         */
        validateFocusName(){
            try {        
                if(format.checkEmptyData(this.emp.employeeName)){
                this.emptyName = true;
                }else this.emptyName = false;
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Hàm kiểm tra rỗng khi focus vào input department
         * author: LTQN(10/9/2022)
         *
         */
        comboboxFocus(){
            try {        
                if(format.checkEmptyData(this.emp.departmentName)){
                this.emptyDepartment = true;
                }else this.emptyDepartment = false;
            } catch (error) {
                console.log(error)
            }
        },
        /**
         * Hàm lấy mã nhân viên mới nhất
         * author: LTQN(19/9/2022)
         */
        getMaxCode(){
            try {

                fetch( this.Url + 'Employees/new-code')
                .then(res => res.text())
                .then(data => {
                    this.emp.employeeCode = data;
                    this.emptyCode = false;
                })
                .catch(error => {
                    console.log(error);
                    return null;
                });
               
            } catch (error) {
                console.log(error)
            }
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
         * Lấy vị trí con chuột cho hover
         * author: LTQN(2/10/2022)
         * @param {*} event 
         */
        getPosition(event){
            this.posX = event.pageX - 10;
            this.posY = event.pageY + 20;
        },

         /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip(){
            this.$emit('hideTooltip');
        },

        /**
         * Đóng form
         * author: LTQN(6/10/2022)
         */
        closeForm(){
            this.change = false;
            for(let key in this.emp){
                if( this.emp[key] !== this.oldEmp[key]){
                    this.change = true;
                    break;
                }
            }
            if(this.change)
                this.$emit('openPopUpChange');
            else this.sendMessageClose();
        }

        
    },
    data(){
        return{
            emp: {}, //đối tượng được chọn
            emptyCode: false,
            emptyName: false,
            emptyDepartment: false,
            formatEmail: true,
            focusDepartment: false,
            picked: null, //link đến radio input
            pickedStatus: null,
            dataCombobox: {},
            dataComboboxPosition: {},
            format: {},
            enumeration: {},
            Url: process.env.VUE_APP_URL,
            checkedEmployee: [], //check box
            posX: null,
            posY: null,
            topForm: null,
            leftForm: null,
            change: false, //kiểm tra sự thay đổi
            oldEmp: {},
            isShowEditWork: false,
            isShowWork:false,
            employeeCode: null,
            showFile: false,
            oldValuePos: "",
            oldValueDep:""
        }
    },
    watch:{
        focus(){
            if(this.emptyDepartment && !this.emptyName && !this.emptyCode){
                this.focusDepartment =! this.focusDepartment;
            }
            if(this.emptyName){
                this.$refs['Name'].focus();
            }
            if(this.emptyCode){
                this.$refs['Code'].focus();
            }
            if(!this.formatEmail){
                this.$refs['Email'].focus();
            }
        },
        saveForm(){
            this.save(enumeration.SaveForm.Save);
        }
    }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);

</style>
  