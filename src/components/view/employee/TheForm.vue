
<template>
    <div class="form-container">
        <div class="container"></div>
        <div class="form" >
        <div class="form-header">
            <div class="icon-form">
                <div class="info-more">
                    <MTooltip :content="'Thông tin chi tiết nhân viên'" :stand="'up'"/>
                </div>
                <div class="icon-x" @click="sendMessageClose"></div>
            </div>
            <div class="form-title">
                <div class="title-employee">Thông tin nhân viên</div>
                <div class="customer">
                    <input type="checkbox" value="0" v-model="checkedEmployee">Là khách hàng
                </div>
                <div class="producer">
                    <input type="checkbox" value="1" v-model="checkedEmployee">Là nhà cung cấp
                </div>
 
            </div>
        </div>
        
        <div class="form-body">
            <div class="row">
                <div class="col-left">
                    <div class="employee-code">
                        <label for="">Mã <span> *</span></label>
                        <input type="text" class="input" placeholder="Mã nhân viên" 
                        @keyup = 'validateFocusCode'
                        @focus = 'validateFocusCode'
                        :ref="'Code'" v-model='emp.employeeCode' :class="{'border-red': emptyCode}"/>
                        <div class="not-empty emptyCode" v-show="emptyCode">Không được để trống.</div>
                    </div>
                    <div class="name">
                        <label for="">Tên <span> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên"
                        @focus = 'validateFocusName' 
                        @keyup="validateFocusName"  v-model='emp.employeeName' :class="{'border-red': emptyName}">
                        <div class="not-empty emptyName" v-show="emptyName">Không được để trống.</div>
                    </div>
                </div>
                <div class="col-right">
                    <div class="date-of-birth">
                        <label for="">Ngày sinh</label>
                        <!-- <input type="date" class="input" v-model="emp.dateOfBirth" name="date"> -->
                        <Datepicker class="datepicker" textInput v-model="emp.dateOfBirth" placeholder="Ngày-Tháng-Năm" format = 'dd-MM-yyyy'></Datepicker>
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
                    <div class="department">
                        <label for="">Đơn vị <span> *</span></label>
                        <MCombobox :url="Url + 'Department'" 
                        :text="'DepartmentName'" 
                        @objectItemCombobox='objectItemCombobox'
                        :valueRender='dataCombobox'
                        :key = 'keyCombobox'/>
                     
                    </div>
                </div>
                <div class="col-right">
                    <div class="identify-number">
                        <label for="">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.identityNumber">
                  
                    </div>
                    <div class="identity-issued-date">
                        <label for="">Ngày cấp</label>
                        <!-- <input type="date" class="input" v-model="emp.identityDate"> -->
                        <Datepicker class="datepicker" textInput v-model="emp.identityDate" placeholder="Ngày-Tháng-Năm" format = 'dd-MM-yyyy'></Datepicker>
                    </div>
                </div>
                
            
            </div>
            <div class="row">
                <div class="col-left">
                    <div class="position">
                        <label for="">Chức danh</label>
                        <input type="text" class="input" placeholder="Chức vụ" v-model="emp.position">
                    </div>
                </div>
                <div class="col-right">
                    <div class="identity-issued-place">
                        <label for="">Nơi cấp</label>
                        <input type="text" class="input" placeholder="Nơi cấp chứng minh nhân dân" v-model="emp.identityPlace">
                    </div>
                </div>
            
            </div>
            <div class="row">
                <div class="address">
                    <label for="">Địa chỉ</label>
                    <input type="text" class="input" placeholder="Địa chỉ liên lạc" v-model="emp.address">
                </div>
            </div>
            <div class="row">
                <div class="phone-number">
                    <label for="">Điện thoại di động</label>
                    <input type="text" class="input" placeholder="Số điện thoại di động" v-model="emp.phoneNumber">
                </div>
                <div class="landline-phone">
                    <label for="">Điện thoại cố định</label>
                    <input type="text" class="input" placeholder="Số điện thoại cố định" v-model="emp.landlinePhone">
                </div>
                <div class="email">
                    <label for="">Email</label>
                    <input type="text" class="input" placeholder="Email" v-model="emp.email">
                </div>
            </div>
            <div class="row">
                <div class="account-bank">
                    <label for="">Tài khoản ngân hàng</label>
                    <input type="text" class="input" placeholder="Tài khoản ngân hàng" v-model="emp.accountBank">
                </div>
                <div class="name-of-bank">
                    <label for="">Tên ngân hàng</label>
                    <input type="text" class="input" placeholder="Tên nhân hàng" v-model="emp.nameOfBank">
                </div>
                <div class="branch-of-bank">
                    <label for="">Chi nhánh</label>
                    <input type="text" class="input" placeholder="Chi nhánh ngân hàng" v-model="emp.branchOfBank">
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

</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
import MCombobox from '@/components/base/MCombobox.vue';
import format from '@/common/formatData';
import Enumeration from '@/common/Enumeration';
import Resource from '../../../common/Resource';
import MTooltip from '@/components/base/MTooltip.vue';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';


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
        if(!validateEmail){
            pointer.form.$emit('warningEmail');
            return false;
        }
        return true;
    }


  export default {
    components: { MButton, MCombobox, MTooltip, Datepicker },
    props: {
        employee: Object,
        mode: Number
    },
    created(){
        try {
            this.enumeration = Enumeration;
            this.emp = this.employee;
            //lấy mã nhân viên lớn nhất
            if(this.mode === Enumeration.Mode.ADD)
                this.getMaxCode();
            
            //xử lý dữ liệu date
            if(!format.checkEmptyData(this.emp.dateOfBirth))
                this.emp.dateOfBirth = this.emp.dateOfBirth.slice(0,10);
            if(!format.checkEmptyData(this.emp.identityDate)){
                this.emp.identityDate = this.emp.identityDate.slice(0,10);
            }
            //xử lý dữ liệu radio
            if(!format.checkEmptyData(this.emp.gender))
                this.picked = this.emp.gender;

            //xử lý dữ liệu combobox
            if(!format.checkEmptyData(this.emp.departmentName)){
                this.dataCombobox = this.emp.departmentName;
                this.emptyDepartment = false;
            }

            //xử lý dữ liệu checkbox
            if(!format.checkEmptyData(this.emp.duty)){
                if(this.emp.duty === 2){
                    this.checkedEmployee[0] = '0';
                    this.checkedEmployee[1] = '1';
                }else{
                    this.checkedEmployee[0] = this.emp.duty.toString();
                }
            }
            
            //xử lý dữ liệu tên nhân viên
            if(format.checkEmptyData(this.emp.employeeName)) //nếu rỗng
                this.emptyName = true;


        } catch (error) {
            console.log(error)
        }    
    },
    mounted(){
            //focus vào mã nhân viên
        this.$refs['Code'].focus();
       
    },
    updated(){
        try {

            //xử lý khi chọn dữ liệu radio
            this.emp.gender = Number(this.picked);
            if(this.emp.gender === Enumeration.gender.Female){
                this.emp.genderName = 'Nữ';
            }
            if(this.emp.gender === Enumeration.gender.Male){
                this.emp.genderName = 'Nam';
            }
            if(this.emp.gender === Enumeration.gender.Other){
                this.emp.genderName = 'Khác';
            }

            //xử lý khi chọn dữ liệu checkbox
            if(this.checkedEmployee.length === 2)
                this.emp.duty = 2;
            else{
                this.emp.duty = Number(this.checkedEmployee[0]);
            }
        } catch (error) {
            console.log(error)
        }

    },
    methods: {
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
                let me = {form: this, email: this.emp.email},
                response = {};
                let objectEmpty = {
                    'EmptyCode' : this.emptyCode, 'EmptyName' : this.emptyName, 'EmptyDepartment' : this.emptyDepartment
                }
                let valid = validate(me, objectEmpty);
                if(valid){
                    if(this.mode === Enumeration.Mode.ADD){ //nếu là thêm
                        response = await fetch(`${this.Url}Employees`,{
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        method: "POST",
                        body: JSON.stringify(this.emp)
                        })
                    }
                    if(this.mode === Enumeration.Mode.EDIT){ //nếu là sửa
                        let url= `${this.Url}Employees/` + this.emp.employeeID;
                        response = await fetch(url, {
                        method: 'PUT',
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        body: JSON.stringify(this.emp),
                        }) 
                        console.log(this.emp);
                    }
                    // thực hiện thành công
                    if(response.ok){
                        this.$emit('openToast', Resource.ToastMessage.success);
                        this.$emit('reload');

                        //Thành công thì đóng form
                        if(mode === Enumeration.SaveForm.SaveAndAdd){ //nếu là cất và thêm
                        this.$emit('reLoadForm');
                        }
                        if(mode === Enumeration.SaveForm.Save){ //nếu là cất
                            this.$emit('closeForm');
                        }
                    } //thực hiện thất bại
                    else{ //lỗi khác: >=400 && <600
                        let data = await response.json()
                        this.$emit('warningErrorSql', data.userMsg); //thông báo cho người dùng
                        console.log(data.devMsg); //thông báo cho dev
                        this.$emit('openToast', Resource.ToastMessage.error);
                    }

                    
                }
            } catch (error) {
                console.log(error)
            }
           
           
        },

         /**Hàm lấy giá trị của combobox
          * author: LTQN(10/9/2022)
          * @param {*} e : giá trị selected trong combobox
          */
        objectItemCombobox(e){
            try {
                this.emp.departmentID = e.DepartmentID;
                this.emp.departmentName = e.DepartmentName;
                this.emp.departmentCode = e.DepartmentCode;
                this.empDepartmentName = this.emp.departmentName;
                
                // kiểm tra rỗng
                if(format.checkEmptyData(this.emp.departmentName)){
                    this.emptyDepartment = true;
                }else  this.emptyDepartment = false;
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
        }

    },
    data(){
        return{
            emp:{}, //đối tượng được chọn
            emptyCode: false,
            emptyName: false,
            emptyDepartment: true,
            picked: null, //link đến radio input
            dataCombobox: '',
            keyCombobox: null,
            format: {},
            enumeration: {},
            Url: process.env.VUE_APP_URL,
            checkedEmployee: [] //check box
            
        }
    },
    
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);

</style>
  