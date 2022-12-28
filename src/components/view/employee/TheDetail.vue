<template>
    <div class="employee-detail">
        <div class="container-detail">
            <h2>THÔNG TIN NHÂN VIÊN</h2>
            <div class="detail" :class="{'edit-detail': stateForm==1}">
                <div style="display:flex; align-items: center;">
                    <h3 style="margin-right: 8px">Thông tin cá nhân</h3>
                    <label class="labelWork" @click="edit"><div class="icon-pen"></div>Sửa thông tin nhân viên</label>
                </div>

                <div class="column" >
                    <div class="column1">
                        <div class="container-input">
                            <div class="label">Mã nhân viên</div>
                                <input disabled type="text" class="input-detail" v-model="emp.employeeCode">

                            </div>
                            <div class="container-input">
                                <div  class="label">Tên nhân viên</div>
                            <input disabled type="text" class="input-detail" v-model="emp.employeeName">

                            </div>
                            <div class="container-input">
                                <div  class="label">Ngày sinh</div>
                            <input disabled v-if="stateForm==0" type="text" class="input-detail" :placeholder="formatDate(emp.dateOfBirth)">
                            <Datepicker v-if="stateForm==1" class="datepicker" :enableTimePicker="false" 
                            v-model="emp.dateOfBirth" placeholder="DD-MM-YYYY" textInput
                            format = 'dd-MM-yyyy' locale="vi" autoApply
                            :dayNames="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"></Datepicker>
                            </div>
                            <div class="container-input">
                                <div class="label">Giới tính</div>
                                <div class="gender" v-if="stateForm==1">
                                    <div class="radio">
                                        <input class="input-radio" type="radio" name="gender" value="0" v-model="picked">Nam
                                        <input class="input-radio" type="radio" name="gender" value="1"  v-model="picked">Nữ
                                        <input class="input-radio" type="radio" name="gender" value="2"  v-model="picked">Khác
                                    </div>
                                </div>
                            <input disabled v-if="stateForm==0" type="text" class="input-detail" v-model="emp.genderName">

                            </div>
                            <div class="container-input">
                                <div  class="label">Số CMND</div>
                            <input :disabled="stateForm==0" type="text" class="input-detail" v-model="emp.identityNumber">

                            </div>
                            <div class="container-input">
                                <div  class="label">Địa chỉ</div>
                            <input :disabled="stateForm==0" type="text" class="input-detail" v-model="emp.address">

                            </div>
                    </div>
                    <div class="column2">
                        <div class="container-input">
                            <div  class="label">Email</div>
                        <input :disabled="stateForm==0" type="text" class="input-detail" v-model="emp.email">

                        </div>
                        <div class="container-input">
                            <div  class="label">Phòng ban</div>
                        <input disabled type="text" class="input-detail" v-model="emp.departmentName">

                        </div>
                        <div class="container-input">
                            <div  class="label">Vị trí</div>
                        <input disabled type="text" class="input-detail" v-model="emp.positionName">

                        </div>
                        <div class="container-input">
                            <div  class="label">Số điện thoại</div>
                        <input :disabled="stateForm==0" type="text" class="input-detail" v-model="emp.phoneNumber">

                        </div>
                        <div class="container-input">
                            <div  class="label">Tình trạng làm việc</div>
                        <input disabled type="text" class="input-detail" v-model="emp.statusName">

                        </div>
                        <div class="container-input">
                        <label class="labelWork" @click="viewHistoryWork"><div class="icon-view"></div>Xem thông tin lịch sử công việc</label>
                        </div>
                    </div>
                </div>
                <div class="buttom-action" v-if="stateForm==1">
                    <div class="cancel" style="margin-right: 16px;">
                    <MButton class="btn-cancel" :text="'Hủy'" @click="cancel"/>
                        </div>
                    <div class="save">
                        <MButton class="btn-save-and-add" :text="'Lưu'" @click="save"/>

                    </div>
                </div>
                </div>
            <div class="detail" :class="{'edit-detail': stateFormSalary==1}" style="margin-top: 30px;">
                <div class="detail-title" style="display:flex; align-items: center;">
                    <h3 style="margin-right: 8px">Thông tin lương</h3>
                    <div class="date-salary">
                        <Datepicker class="datepicker" :enableTimePicker="false" 
                            v-model="daySalary" placeholder="MM-YYYY" textInput
                            format = 'MM-yyyy' locale="vi" autoApply
                            :dayNames="['T2', 'T3', 'T4', 'T5', 'T6', 'T7', 'CN']"></Datepicker>
                    </div> 
                    <MButton class="btn-save-and-add" :text="'Xem'" @click="getSalaryByDay"/>
                </div>
                <label class="labelWork" @click="editBank"><div class="icon-pen"></div>Sửa thông ngân hàng</label>
                <div class="column">
                    <div class="column1">
                        <div class="container-input">
                                <div  class="label">Lương cơ bản</div>
                            <input disabled type="text" class="input-detail" v-model="sal.salaryMonthly">

                        </div>
                        <div class="container-input">
                                <div  class="label">Lương thưởng</div>
                            <input disabled type="text" class="input-detail" v-model="sal.bonus">

                        </div>
                        <div class="container-input">
                                <div  class="label">Ứng trước</div>
                            <input disabled type="text" class="input-detail" v-model="sal.advanceMoney">

                        </div>
                        <div class="container-input">
                                <div  class="label">Phụ cấp (Tiền vé gửi xe)</div>
                            <input disabled type="text" class="input-detail" v-model="sal.allowance">

                        </div>
                        <div class="container-input">
                                <div  class="label">Ngày công</div>
                            <input disabled type="text" class="input-detail" v-model="sal.numberWork">

                        </div>
                       
                    </div>
                    <div class="column2">
                        <div class="container-input">
                                <div  class="label">Ngày làm thêm</div>
                            <input disabled type="text" class="input-detail" v-model="sal.bonusDate">

                        </div>
                        <div class="container-input">
                                <div  class="label">Tổng lương</div>
                            <input disabled type="text" class="input-detail" v-model="salary">

                        </div>
                        <div class="container-input">
                                <div  class="label">Tên ngân hàng</div>
                            <input :disabled="stateFormSalary==0" type="text" class="input-detail" v-model="sal.nameOfBank">

                        </div>
                        <div class="container-input">
                                <div  class="label">Số tài khoản</div>
                            <input :disabled="stateFormSalary==0" type="text" class="input-detail" v-model="sal.accountBank">

                        </div>
                        <div class="container-input">
                                <div  class="label">Chi nhánh ngân hàng</div>
                            <input :disabled="stateFormSalary==0" type="text" class="input-detail" v-model="sal.branchOfBank">

                        </div>
                    </div>
                    
                </div>
                <div class="buttom-action" v-if="stateFormSalary==1">
                    <div class="cancel" style="margin-right: 16px;">
                    <MButton class="btn-cancel" :text="'Hủy'" @click="cancelSalary"/>
                        </div>
                    <div class="save">
                        <MButton class="btn-save-and-add" :text="'Lưu'" @click="saveSalary"/>

                    </div>
                </div>
            </div>
        </div>
       
    </div>
    <TheTableHistoryWorkVue v-if="isShowWork" :employeeCode = "emp.employeeCode" @closeForm="closeFormWork"/>
</template>
  
<script>
import MButton from '@/components/base/MButton.vue';
import enumeration from '@/common/enumeration';
import format from '@/common/formatData';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import TheTableHistoryWorkVue from '../historyWork/TheTableHistoryWork.vue';
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
                // pointer.form.$emit('openToast', resource.ToastMessageType.success, resource.ToastMessage.success);
                // pointer.form.$emit('reload');
                pointer.stateForm = 0;
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                let data = await res.json();
                console.log(data);
                // pointer.form.$emit('warningError', data); //thông báo cho người dùng
                // pointer.form.$emit('openToast', resource.ToastMessageType.error, resource.ToastMessage.error);
            }
    
        } catch (error) {
            console.log(error);
        }
      
    }
  export default {
    components: { MButton, Datepicker, TheTableHistoryWorkVue},
    props:{
        code: String
    },
    created(){
        let url = process.env.VUE_APP_URL + "Employees/code/" + this.code;
            fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.emp = res[0];              
                     //xử lý dữ liệu radio
                if(!format.checkEmptyData(this.emp.gender))
                    this.picked = this.emp.gender;

                    sessionStorage.setItem('managedep', this.emp.departmentName);
                }).catch(error => {
                  console.log(error);
            }) 

        let now = new Date();
        let day = (now.getMonth() + 1).toString() + "N" + now.getFullYear().toString();
        url = process.env.VUE_APP_URL + "Salaries/code/" + this.code + "/" + day;
            fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.sal = res[0];              
                     this.salary = Math.round((this.sal.salaryMonthly/26).toFixed(0) * (this.sal.numberWork + this.sal.dayoff + this.sal.bonusDate + this.sal.bonusDate*0.5)) + this.sal.bonus - this.sal.advanceMoney + this.sal.allowance;
                
                }).catch(error => {
                  console.log(error);
            }) 

            
    },
    update(){
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
    },
    methods:{
        viewHistoryWork(){
            this.isShowWork = true;
        },
        closeFormWork(){
            this.isShowWork = false;
        },
        edit(){
            this.stateForm = 1;
        },
        cancel(){
            this.stateForm = 0;
        },
        cancelSalary(){
            this.stateFormSalary = 0;
        },
        formatDate(e){
            if(!format.checkEmptyData(e)){
                return format.formatDate(e);
            }
        },
        async saveSalary(){
            let url= process.env.VUE_APP_URL +"Salaries/" + this.sal.salaryID;
            let response = await fetch(url, {
            method: 'PUT',
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            body: JSON.stringify(this.sal),
            })

            if(response.ok){
                // this.$emit('openToast', resource.ToastMessageType.success, resource.ToastMessage.success);
                // this.$emit('reload');
                // this.$emit('closeForm');
                this.stateFormSalary = 0;
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                // let data = await response.json();
                // this.$emit('warningError', data); //thông báo cho người dùng
                // this.$emit('openToast', resource.ToastMessageType.error, resource.ToastMessage.error);
            }
        },
        editBank(){
            this.stateFormSalary = 1;
        },
        async save(){
            let me = this;
            let url= `${this.Url}Employees/` + this.emp.employeeID;
            let response = await fetch(url, {
            method: 'PUT',
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            body: JSON.stringify(this.emp),
            }) 
            handleResponse(response, me);
            this.stateForm = 0;

        },
        getSalaryByDay(){
            let day = (this.daySalary.getMonth() + 1).toString() + "N" + this.daySalary.getFullYear().toString();
            let url = process.env.VUE_APP_URL + "Salaries/code/" + this.code + "/" + day;
                fetch(url)
                    .then(res => res.json())
                    .then(res => {
                        if(res.length > 0){
                            this.sal = res[0];             
                            this.salary = Math.round((this.sal.salaryMonthly/26).toFixed(0) * (this.sal.numberWork + this.sal.dayoff + this.sal.bonusDate + this.sal.bonusDate*0.5)) + this.sal.bonus - this.sal.advanceMoney + this.sal.allowance;
                            
                        }else{
                            this.sal = [];
                            this.salary = null;
                        }
                    }).catch(error => {
                    console.log(error);
                }) 

            }
    },
    data(){
        return{
            emp: {},
            sal: {},
            stateForm: 0,
            Url: process.env.VUE_APP_URL,
            picked: null, //link đến radio input
            pickedStatus: null,
            stateFormSalary: 0,
            isShowWork: false,
            salary: null,
            daySalary: null
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/layout/detail.css)

  </style>
  