
<template>
    <div class="form-container">
        <div class="container"></div>
        <div class="form" >
        <div class="form__header">
            <div class="icon-form">
                <div class="info-more"></div>
                <div class="icon-x" @click="sendMessageClose"></div>
            </div>
            <div class="form__title">
                <div class="title-employee">Thông tin nhân viên</div>
                <div class="customer">
                    <input type="checkbox" name="" id="">Là khách hàng
                </div>
                <div class="producer">
                    <input type="checkbox" name="" id="">Là nhà cung cấp
                </div>
            </div>
        </div>
        
        <div class="form__body">
            <div class="row">
                <div class="col-left">
                    <div class="employeeCode">
                        <label for="">Mã <span> *</span></label>
                        <input type="text" class="input" placeholder="Mã nhân viên" @focus="validateFocusCode" :ref="'Code'" v-model='emp.EmployeeCode' :class="{'empty': emptyCode}">
                        <div class="notEmpty" v-show="emptyCode">Không được để trống.</div>
                    </div>
                    <div class="name">
                        <label for="">Tên <span> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên" @focus="validateFocusName"  v-model='emp.FullName' :class="{'empty': emptyName}">
                        <div class="notEmpty" v-show="emptyName">Không được để trống.</div>
                    </div>
                </div>
                <div class="col-right">
                    <div class="dateOfBirth">
                        <label for="">Ngày sinh</label>
                        <input type="date" class="input" v-model="emp.DateOfBirth">
                    </div>
                    <div class="gender">
                        <label for="">Giới tính</label>
                        <div class="radio">
                            <input type="radio" name="gender" value="0" v-model="picked">Nam
                            <input type="radio" name="gender" value="1"  v-model="picked">Nữ
                            <input type="radio" name="gender" value="2"  v-model="picked">Khác
                        </div>
                    </div>
                </div>
                
                
            </div>
            <div class="row">
                <div class="col-left">
                    <div class="department">
                        <label for="">Đơn vị <span> *</span></label>
                        <MCombobox :url="'https://cukcuk.manhnv.net/api/v1/Departments'" 
                        :text="'DepartmentName'" 
                        @objectItemCombobox='objectItemCombobox'
                        :valueRender='dataCombobox'
                        :key = 'keyCombobox'/>
                     
                    </div>
                </div>
                <div class="col-right">
                    <div class="identifyNumber">
                        <label for="">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.IdentityNumber">
                  
                    </div>
                    <div class="identityIssuedDate">
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
                    <div class="identityIssuedPlace">
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
                <div class="phoneNumber">
                    <label for="">Điện thoại di động</label>
                    <input type="text" class="input" placeholder="Số điện thoại di động" v-model="emp.PhoneNumber">
                </div>
                <div class="landlinePhone">
                    <label for="">Điện thoại cố định</label>
                    <input type="text" class="input" placeholder="Số điện thoại cố định" v-model="emp.PhoneNumber">
                </div>
                <div class="email">
                    <label for="">Email</label>
                    <input type="text" class="input" placeholder="Email" v-model="emp.Email">
                </div>
            </div>
            <div class="row">
                <div class="accountBank">
                    <label for="">Tài khoản ngân hàng</label>
                    <input type="text" class="input" placeholder="Tài khoản ngân hàng" v-model="emp.PersonalTaxCode">
                </div>
                <div class="nameOfBank">
                    <label for="">Tên ngân hàng</label>
                    <input type="text" class="input" placeholder="Tên nhân hàng" v-model="emp.QualificationName">
                </div>
                <div class="branchOfBank">
                    <label for="">Chi nhánh</label>
                    <input type="text" class="input" placeholder="Chi nhánh ngân hàng" v-model="emp.EducationalBackground">
                </div>
            </div>
        </div>
        <div class="form__footer">
            <div class="cancle">
                <MButton class="btn-cancle" :text="'Hủy'" @click="sendMessageClose"/>
            </div>
            <div class="save">
                <MButton class="btn-save" :text="'Cất'" @click="save(SaveForm.Save)"/>
                <MButton class="btn-saveAndAdd" :text="'Cất và thêm'" @click="save(SaveForm.SaveAndAdd)"/>
            </div>
        </div>
    </div>

    </div>
    
</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
import MCombobox from '@/components/base/MCombobox.vue';
    /**
     * Hàm validate
     * author: LTQN(12/9/2022)
     * @param {*} that: con trỏ vue
     * @param {boolean} emptyCode 
     * @param {boolean} emptyName 
     * @param {boolean} emptyDepartment 
     * @
     */
   function validate(that, emptyCode, emptyName, emptyDepartment){
        if(emptyCode){ // nếu mã nhân viên rỗng
            that.$emit('warningEmptyCode');
            return false;
        }
        if(emptyName){
            that.$emit('warningEmptyName');
            return false;
        }
        if(emptyDepartment){
            that.$emit('warningEmptyDepartment');
            return false;
        }
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
        
        return true;
   }
  export default {
    components: { MButton, MCombobox },
    props: {
        employee: Object,
        mode: Number
    },
    created(){
        this.emp = this.employee;
        //xử lý dữ liệu date
        if(this.emp.DateOfBirth !=null)
            this.emp.DateOfBirth = this.emp.DateOfBirth.slice(0,10);
        if(this.emp.IdentityDate !=null)
            this.emp.IdentityDate = this.emp.identityDate.slice(0,10);
        //xử lý dữ liệu radio
        if(this.emp.Gender !=null)
            this.picked = this.emp.Gender;
        //xử lý dữ liệu combobox
        if(this.emp.DepartmentName !=null)
            this.dataCombobox = this.emp.DepartmentName;

        
    },
    mounted(){
            //focus vào mã nhân viên
        this.$refs['Code'].focus();
       
    },
    updated(){
        this.validateCode = this.emp.EmployeeCode;
        this.validateName = this.emp.FullName;


        this.emp.Gender = this.picked;
        if(this.picked == this.gender.Female){
            this.emp.GenderName = 'Nữ';
        }
        if(this.picked == this.gender.Male){
            this.emp.GenderName = 'Name';
        }
        if(this.picked == this.gender.Other){
            this.emp.GenderName = 'Khác';
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
            //validate
            let me = this;
            let valid = validate(me, this.emptyCode, this.emptyName, this.emptyDepartment);
            if(valid){
                if(this.mode == this.Mode.ADD){ //nếu là thêm
                    await fetch("https://cukcuk.manhnv.net/api/v1/Employees",{
                    headers: {'Accept': 'application/json','Content-Type': 'application/json'},
                    method: "POST",
                    body: JSON.stringify(this.emp)
                    })
                }
                if(this.mode == this.Mode.EDIT){ //nếu là sửa
                    let url= "https://cukcuk.manhnv.net/api/v1/Employees/" + this.emp.EmployeeId;
                    await fetch(url, {
                    method: 'PUT',
                    headers: {'Accept': 'application/json','Content-Type': 'application/json',
                    },
                    body: JSON.stringify(this.emp),
                    })
                }
                if(mode == this.SaveForm.SaveAndAdd){
                    this.emp = {};
                    this.dataCombobox = '';
                    this.keyCombobox =  Math.floor(Math.random()*100000);
                }
                if(mode == this.SaveForm.Save){
                    this.$emit('closeForm');
                }
                this.$emit('reload');
            }
           
        },
        /**
         * Hàm lấy giá trị của combobox
         * author: LTQN(10/9/2022)
         *
         */
        objectItemCombobox(e){
            this.emp.DepartmentId = e.DepartmentId;
            this.emp.DepartmentName = e.DepartmentName;
            this.emp.DepartmentCode = e.DepartmentCode;
            this.empDepartmentName = this.emp.DepartmentName;
               
            // kiểm tra rỗng
            if(this.emp.DepartmentName == undefined || this.emp.DepartmentName == ''){
                this.emptyDepartment = true;
            }else  this.emptyDepartment = false;
 
        },
        /**
         * Hàm kiểm tra rỗng khi focus vào input employee Code
         * author: LTQN(10/9/2022)
         *
         */
        validateFocusCode(){
            if(this.emp.EmployeeCode == undefined || this.emp.EmployeeCode == ''){
            this.emptyCode = true;
            }else  this.emptyCode = false;
          
        },
        /**
         * Hàm kiểm tra rỗng khi focus vào input fullname
         * author: LTQN(10/9/2022)
         *
         */
        validateFocusName(){
            if(this.emp.FullName == undefined || this.emp.FullName == ''){
                this.emptyName =true;
            }else this.emptyName = false;
        }
    },
    data(){
        return{
            emp:{}, //đối tượng được chọn
            emptyCode: false,
            emptyName: false,
            emptyDepartment: true,
            validateCode: '',
            validateName: '',
            Mode : {
                ADD: 1,
                EDIT: 2
            },
            picked: null, //link đến radio input
            gender: {
                Male: 0,
                Female: 1,
                Other: 2
            },
            dataCombobox: '',
            SaveForm :{
                Save: 1,
                SaveAndAdd: 2
            },
            keyCombobox: null
        }
    },
    watch: {
        validateCode(oldValue){
            if(oldValue != undefined){
                // console.log(oldValue + ":"+ newValue);
                if(oldValue == ''){
                    this.emptyCode = true;
                }else  this.emptyCode = false;
            }
        },
        validateName(oldValue){
            if(oldValue != undefined){
                if(oldValue == ''){
                    this.emptyName =true;
                }else this.emptyName = false;
            }
        },
    }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);

</style>
  