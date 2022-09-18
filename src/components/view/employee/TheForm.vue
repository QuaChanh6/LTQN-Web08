
<template>
    <div class="form-container">
        <div class="container"></div>
        <div class="form" >
        <div class="form-header">
            <div class="icon-form">
                <div class="info-more"></div>
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
                        :ref="'Code'" v-model='emp.EmployeeCode' :class="{'border-red': emptyCode}">
                        <div class="not-empty" v-show="emptyCode">Không được để trống.</div>
                    </div>
                    <div class="name">
                        <label for="">Tên <span> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên"
                        @focus = 'validateFocusName' 
                        @keyup="validateFocusName"  v-model='emp.FullName' :class="{'border-red': emptyName}">
                        <div class="not-empty" v-show="emptyName">Không được để trống.</div>
                    </div>
                </div>
                <div class="col-right">
                    <div class="date-of-birth">
                        <label for="">Ngày sinh</label>
                        <input type="date" class="input" v-model="emp.DateOfBirth" name="date">

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
                        <MCombobox :url="Url + 'Departments'" 
                        :text="'DepartmentName'" 
                        @objectItemCombobox='objectItemCombobox'
                        :valueRender='dataCombobox'
                        :key = 'keyCombobox'/>
                     
                    </div>
                </div>
                <div class="col-right">
                    <div class="identify-number">
                        <label for="">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.IdentityNumber">
                  
                    </div>
                    <div class="identity-issued-date">
                        <label for="">Ngày cấp</label>
                        <input type="date" class="input" v-model="emp.IdentityDate">
                    </div>
                </div>
                
            
            </div>
            <div class="row">
                <div class="col-left">
                    <div class="position">
                        <label for="">Chức danh</label>
                        <input type="text" class="input" placeholder="Chức vụ" v-model="emp.PositionName">
                    </div>
                </div>
                <div class="col-right">
                    <div class="identity-issued-place">
                        <label for="">Nơi cấp</label>
                        <input type="text" class="input" placeholder="Nơi cấp chứng minh nhân dân" v-model="emp.IdentityPlace">
                    </div>
                </div>
            
            </div>
            <div class="row">
                <div class="address">
                    <label for="">Địa chỉ</label>
                    <input type="text" class="input" placeholder="Địa chỉ liên lạc" v-model="emp.Address">
                </div>
            </div>
            <div class="row">
                <div class="phone-number">
                    <label for="">Điện thoại di động</label>
                    <input type="text" class="input" placeholder="Số điện thoại di động" v-model="emp.PhoneNumber">
                </div>
                <div class="landline-phone">
                    <label for="">Điện thoại cố định</label>
                    <input type="text" class="input" placeholder="Số điện thoại cố định" v-model="emp.PhoneNumber">
                </div>
                <div class="email">
                    <label for="">Email</label>
                    <input type="text" class="input" placeholder="Email" v-model="emp.Email">
                </div>
            </div>
            <div class="row">
                <div class="account-bank">
                    <label for="">Tài khoản ngân hàng</label>
                    <input type="text" class="input" placeholder="Tài khoản ngân hàng" v-model="emp.PersonalTaxCode">
                </div>
                <div class="name-of-bank">
                    <label for="">Tên ngân hàng</label>
                    <input type="text" class="input" placeholder="Tên nhân hàng" v-model="emp.QualificationName">
                </div>
                <div class="branch-of-bank">
                    <label for="">Chi nhánh</label>
                    <input type="text" class="input" placeholder="Chi nhánh ngân hàng" v-model="emp.EducationalBackground">
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



    /**
     * Hàm validate
     * author: LTQN(14/9/2022)
     * @param {*} form: con trỏ vue
     * @param {object} objectEmpty {emptyCode, emptyName, emptyDepartment} 

     * 
     */
    function validate(form, objectEmpty){
        for(let key in objectEmpty){
            if(objectEmpty[key]){
                // form.$emit(`warningEmpty${key}`);
                form.$emit('warningEmpty', key);
                return false;
            }
        }
        return true;

        /**
         * Kiểm tra định dạng email
         */
        // if(email != '' || email != undefined){
        //     //eslint-disable-next-line
        //     let regexEmail= /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        //     if(!regexEmail.test(email)){
        //         return false;
        //     }
        // }
        
        // return true;
   }
  export default {
    components: { MButton, MCombobox},
    props: {
        employee: Object,
        mode: Number
    },
    created(){
        try {
            this.enumeration = Enumeration;
            this.emp = this.employee;
            //lấy mã nhân viên lớn nhất
            // if(this.mode == Enumeration.Mode.ADD)
            //     this.getMaxCode();
            
            //xử lý dữ liệu date
            if(!format.checkEmptyData(this.emp.DateOfBirth))
                this.emp.DateOfBirth = this.emp.DateOfBirth.slice(0,10);
            if(!format.checkEmptyData(this.emp.identityDate)){
                this.emp.identityDate = this.emp.identityDate.slice(0,10);
            }
            //xử lý dữ liệu radio
            if(!format.checkEmptyData(this.emp.Gender))
                this.picked = this.emp.Gender;
            //xử lý dữ liệu combobox
            if(!format.checkEmptyData(this.emp.DepartmentName)){
                this.dataCombobox = this.emp.DepartmentName;
                this.emptyDepartment = false;
            }
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
            this.emp.Gender = this.picked;
            if(this.picked == Enumeration.gender.Female){
                this.emp.GenderName = 'Nữ';
            }
            if(this.picked == Enumeration.gender.Male){
                this.emp.GenderName = 'Name';
            }
            if(this.picked == Enumeration.gender.Other){
                this.emp.GenderName = 'Khác';
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
                let me = this;
                let response = {};
                let objectEmpty = {
                    'EmptyCode' : this.emptyCode, 'EmptyName' : this.emptyName, 'EmptyDepartment' : this.emptyDepartment
                }
                let valid = validate(me, objectEmpty);
                if(valid){
                    if(this.mode == Enumeration.Mode.ADD){ //nếu là thêm
                        response = await fetch(`${this.Url}Employees`,{
                        headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                        method: "POST",
                        body: JSON.stringify(this.emp)
                        })
                    }
                    if(this.mode == Enumeration.Mode.EDIT){ //nếu là sửa
                        let url= `${this.Url}Employees/` + this.emp.EmployeeId;
                        response = await fetch(url, {
                        method: 'PUT',
                        headers: {'Accept': 'application/json','Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.emp),
                        })                 
                    }

                    if(response.ok){
                        this.$emit('openToast', Resource.ToastMessage.success);
                        this.$emit('reload');
                    }else{ //lỗi khác: >=400 && <600
                        this.$emit('openToast', Resource.ToastMessage.error);
                    }
                    if(mode == Enumeration.SaveForm.SaveAndAdd){ //nếu là cất
                        this.emp = {};
                        this.dataCombobox = '';
                        this.keyCombobox =  Math.floor(Math.random()*100000);
                    }
                    if(mode == Enumeration.SaveForm.Save){ //nếu là cất và thêm
                        this.$emit('closeForm');
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
                this.emp.DepartmentId = e.DepartmentId;
                this.emp.DepartmentName = e.DepartmentName;
                this.emp.DepartmentCode = e.DepartmentCode;
                this.empDepartmentName = this.emp.DepartmentName;
                
                // kiểm tra rỗng
                if(format.checkEmptyData(this.emp.DepartmentName)){
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
                if(format.checkEmptyData(this.emp.EmployeeCode)){
                this.emptyCode = true;
                }else  this.emptyCode = false;
            } catch (error) {
                console.log(error)
            }

        },
        /**
         * Hàm kiểm tra rỗng khi focus vào input fullname
         * author: LTQN(10/9/2022)
         *
         */
        validateFocusName(){
            try {        
                if(format.checkEmptyData(this.emp.FullName)){
                this.emptyName = true;
                }else this.emptyName = false;
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Hàm lấy mã nhân viên mới nhất
         */
        // getMaxCode(){
        //     try {

        //         fetch('')
        //         .then(res => res.text())
        //         .then(data => {
        //             this.emp.EmployeeCode = data;
        //             this.emptyCode = false;
        //             console.log(data)
        //         })
        //         .catch(error => {
        //             console.log(error);
        //             return null;
        //         });
               
        //     } catch (error) {
        //         console.log(error)
        //     }
        // }

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
            Url: Resource.Url,
            checkedEmployee: [] //check box
            
        }
    },
    
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);

</style>
  