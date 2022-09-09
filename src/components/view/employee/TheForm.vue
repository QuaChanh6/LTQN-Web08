
<template>
    <div class="form-container" v-if="isShow">
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
                    <!-- <MInput :type="'checkbox'" :placeholderInput="''" :isClassText="false"/><span>Là khách hàng</span> -->
                </div>
                <div class="producer">
                    <input type="checkbox" name="" id="">Là nhà cung cấp
                    <!-- <MInput :type="'checkbox'" :placeholderInput="''" :isClassText="false"/><span>Là nhà cung cấp</span></div> -->
                </div>
            </div>
        </div>
        
        <div class="form__body">
            <div class="row">
                <div class="col-left">
                    <div class="employeeCode">
                        <label for="">Mã <span> *</span></label>
                        <input type="text" class="input" placeholder="Mã nhân viên" v-model='emp.employeeCode'>
                        <!-- <MInput class="" :type="'text'" :placeholderInput="'Mã nhân viên'" :isClassText="true"/> -->
                    </div>
                    <div class="name">
                        <label for="">Tên <span> *</span></label>
                        <input type="text" class="input" placeholder="Tên nhân viên" v-model='emp.FullName'>
                        <!-- <MInput class="" :type="'text'" :value="emp.FullName" :placeholderInput="'Tên nhân viên'" :isClassText="true" v-model='emp.FullName'/> -->
                    </div>
                </div>
                <div class="col-right">
                    <div class="dateOfBirth">
                        <label for="">Ngày sinh</label>
                        <input type="date" class="input" v-model="date">
                    </div>
                    <div class="gender">
                        <label for="">Giới tính</label>
                        <div class="radio">
                            <input type="radio" name="gender" id="0">Nam
                            <input type="radio" name="gender" id="1">Nữ
                            <input type="radio" name="gender" id="2">Khác
                        </div>
                    </div>
                </div>
                
                
            </div>
            <div class="row">
                <div class="col-left">
                    <div class="department">
                        <label for="">Đơn vị <span> *</span></label>
                        <!-- <combobox id="position" api="https://cukcuk.manhnv.net/api/v1/Positions" text="PositionName" value="PositionId"></combobox> -->
                    </div>
                </div>
                <div class="col-right">
                    <div class="identifyNumber">
                        <label for="">Số CMND</label>
                        <input type="text" class="input" placeholder="Số chứng mình nhân dân" v-model="emp.IdentityNumber">
                        <!-- <MInput class="" :type="'text'" :placeholderInput="'Số chứng mình nhân dân'" :isClassText="true"/> -->
                    </div>
                    <div class="identityIssuedDate">
                        <label for="">Ngày cấp</label>
                        <input type="date" class="input" v-model="emp.IdentityDate">
                        <!-- <MInput class="" :type="'date'" :placeholderInput="''" :isClassText="true"/> -->

                    </div>
                </div>
                
            
            </div>
            <div class="row">
                <div class="col-left">
                    <div class="position">
                        <label for="">Chức danh</label>
                        <input type="text" class="input" placeholder="Chức vụ" v-model="emp.PositionName">
                        <!-- <MInput class="" :type="'text'" :placeholderInput="'Chức vụ'" :isClassText="true"/> -->
                    </div>
                </div>
                <div class="col-right">
                    <div class="identityIssuedPlace">
                        <label for="">Nơi cấp</label>
                        <input type="text" class="input" placeholder="Nơi cấp chứng minh nhân dân" v-model="emp.IdentityPlace">
                        <!-- <MInput class="" :type="'text'" :placeholderInput="'Nơi cấp chứng minh nhân dân'" :isClassText="true"/> -->
                    </div>
                </div>
            
            </div>
            <div class="row">
                <div class="address">
                    <label for="">Địa chỉ</label>
                    <input type="text" class="input" placeholder="Địa chỉ liên lạc" v-model="emp.Address">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Địa chỉ liên lạc'" :isClassText="true"/> -->
                </div>
            </div>
            <div class="row">
                <div class="phoneNumber">
                    <label for="">Điện thoại di động</label>
                    <input type="text" class="input" placeholder="Số điện thoại di động" v-model="emp.PhoneNumber">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Số điện thoại di động'" :isClassText="true"/> -->
                </div>
                <div class="landlinePhone">
                    <label for="">Điện thoại cố định</label>
                    <input type="text" class="input" placeholder="Số điện thoại cố định" v-model="emp.PhoneNumber">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Số điện thoại cố định'" :isClassText="true"/> -->
                </div>
                <div class="email">
                    <label for="">Email</label>
                    <input type="text" class="input" placeholder="Email" v-model="emp.Email">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Email'" :isClassText="true"/> -->
                </div>
            </div>
            <div class="row">
                <div class="accountBank">
                    <label for="">Tài khoản ngân hàng</label>
                    <input type="text" class="input" placeholder="Tài khoản ngân hàng" v-model="emp.PersonalTaxCode">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Tài khoản ngân hàng'" :isClassText="true"/> -->
                </div>
                <div class="nameOfBank">
                    <label for="">Tên ngân hàng</label>
                    <input type="text" class="input" placeholder="Tên nhân hàng" v-model="emp.QualificationName">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Tên nhân hàng'" :isClassText="true"/> -->
                </div>
                <div class="branchOfBank">
                    <label for="">Chi nhánh</label>
                    <input type="text" class="input" placeholder="Chi nhánh ngân hàng" v-model="emp.EducationalBackground">
                    <!-- <MInput class="" :type="'text'" :placeholderInput="'Chi nhánh ngân hàng'" :isClassText="true"/> -->
                </div>
            </div>
        </div>
        <div class="form__footer">
            <div class="cancle">
                <MButton class="btn-cancle" :text="'Hủy'" @click="sendMessageClose"/>
            </div>
            <div class="save">
                <MButton class="btn-save" :text="'Cất'"/>
                <MButton class="btn-saveAndAdd" :text="'Cất và thêm'"/>
            </div>
        </div>
    </div>

    </div>
    
</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
  export default {
    components: { MButton },
    props: {
        isShow: Boolean,
        employee: Object
    },
    created(){
        this.emp = this.employee;
        console.log(this.employee);
    },
    methods: {
        sendMessageClose(){
            this.$emit('closeForm');
        },
       
    },
    data(){
        return{
            emp:{},
            date: ''
        }
    }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/form.css);
</style>
  