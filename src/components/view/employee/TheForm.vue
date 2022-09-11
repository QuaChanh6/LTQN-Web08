
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
                        <input type="text" class="input" placeholder="Mã nhân viên" :ref="'Code'" v-model='emp.EmployeeCode' :class="{empty: emptyCode}">
                    </div>
                    <div class="name">
                        <label for="">Tên <span> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên" v-model='emp.FullName' :class="{empty: emptyName}">
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
                        :valueRender='dataCombobox'/>
                     
                    </div>
                </div>
                <div class="col-right">
                    <div class="identifyNumber">
                        <label for="">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.IdentityNumber">
                  
                    </div>
                    <div class="identityIssuedDate">
                        <label for="">Ngày cấp</label>
                        <input type="date" class="input" v-model="emp.identityDate">
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
                <MButton class="btn-save" :text="'Cất'" @click="save"/>
                <MButton class="btn-saveAndAdd" :text="'Cất và thêm'"/>
            </div>
        </div>
    </div>

    </div>
    
</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
import MCombobox from '@/components/base/MCombobox.vue';
    /**
     * Hàm thông bao dữ liệu không đúng định dạng
     */
    // function validate(){
    //     if(this.emp.EmployeeCode == ''){

    //     }
    // }
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
        if(this.emp.identityDate !=null)
            this.emp.identityDate = this.emp.identityDate.slice(0,10);
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
    beforeUpdate(){
        //kiem tra rong
        if(this.emp.EmployeeCode == '')
            this.emptyCode= true;
        else this.emptyCode= false;
        if(this.emp.FullName == '')
            this.emptyName= true; 
        else this.emptyName= false; 

        //xu ly du lieu radio
              
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
         */
        save(){   
            if(this.mode == this.Mode.ADD){
                fetch("https://cukcuk.manhnv.net/api/v1/Employees",{
                    headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                    },
                    method: "POST",
                    body: JSON.stringify(this.emp)
                })
                .then(function(res){ console.log(res) })
                .catch(function(res){ console.log(res) })
            }
            if(this.mode == this.Mode.EDIT){
                let url= "https://cukcuk.manhnv.net/api/v1/Employees/" + this.emp.EmployeeId;
                fetch(url, {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(this.emp),
                })
                .then((res) => res.json())
                .then((res) => {
                    console.log(res);
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
            }
            this.$emit('closeForm');
            this.$emit('reload');
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
        }
    },
    data(){
        return{
            emp:{}, //đối tượng được chọn
            date: '',
            emptyCode: false,
            emptyName: false,
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
            dataCombobox: ''
        }
    },
 
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);
</style>
  