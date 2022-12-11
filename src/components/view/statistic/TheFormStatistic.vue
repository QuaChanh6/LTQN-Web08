<template>
    <div class="container-form-statistic">
        <div class="container"></div>
        <div class="form-statistic">
            <div class="form-header">
                <div class="title-employee">Thông tin tiền lương</div>
                <div class="icon-x" @click="closeForm"></div>
            </div>

            <div class="form-body">
                <div class="row">
                    <div class="nameEmp">Tên nhân viên: <span>{{sal.employeeName}}</span></div>
                </div>
                <div class="row">
                    <div class="salary right">
                        <label>Lương hàng tháng:</label>
                        <input type="text" class="input" placeholder="Lương hàng tháng" v-model="sal.salaryMonthly">
                    </div>
                    <div class="bonus left">
                        <label>Tiền thưởng:</label>
                        <input type="text" class="input" placeholder="Tiền thưởng" v-model="sal.bonus">
                    </div>
                </div>
                <div class="row">
                   <div class="advance-money right">
                        <label>Ứng trước:</label>
                        <input type="text" class="input" placeholder="Ứng trước" v-model="sal.advanceMoney">
                   </div>
                   <div class="fines left">
                        <label>Số ngày công:</label>
                        <input type="text" class="input" placeholder="Số ngày công" v-model="sal.numberWork">
                   </div>
                </div>
                <div class="row">
                    <div class="name-bank right">
                        <label>Tên ngân hàng:</label>
                        <input type="text" class="input" placeholder="Tên ngân hàng" v-model="sal.nameOfBank">
                   </div>
                   <div class="account left">
                        <label>Tài khoản ngân hàng:</label>
                        <input type="text" class="input" placeholder="Tài khoản ngân hàng" v-model="sal.accountBank">
                   </div>
                </div>
                <div class="row">
                   <div class="branch-bank ">
                        <label>Chi nhánh ngân hàng:</label>
                        <input type="text" class="input" placeholder="Chi nhánh ngân hàng" v-model="sal.branchOfBank">
                   </div>
                </div>
            </div>

            <div class="form-footer">
                <div class="cancel">
                    <MButton :text="'Hủy'" @click="closeForm"/>
                </div>
                <div class="save">
                    <MButton :text="'Cất'" @click='editSalary'/>
                   
                </div>
        </div>
        </div>
    </div>
  </template>
  
  <script>
import MButton from '@/components/base/MButton.vue';
import resource from '../../../common/resource';
  export default {
    components: { MButton },
    props: {
        salary: Object
    },
    created(){
        this.sal = this.salary;
    },
    methods:{
        closeForm(){
            this.$emit('closeForm');
        },
        async editSalary(){
            let url= process.env.VUE_APP_URL +"Salaries/" + this.sal.salaryID;
            let response = await fetch(url, {
            method: 'PUT',
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            body: JSON.stringify(this.sal),
            })

            if(response.ok){
                this.$emit('openToast', resource.ToastMessageType.success, resource.ToastMessage.success);
                this.$emit('reload');
                this.$emit('closeForm');
            
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                // let data = await response.json();
                // this.$emit('warningError', data); //thông báo cho người dùng
                this.$emit('openToast', resource.ToastMessageType.error, resource.ToastMessage.error);
            }
    
        }

    },
    data(){
        return{
            sal: {}
        }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/statistic/formStatistic.css);
  </style>
  