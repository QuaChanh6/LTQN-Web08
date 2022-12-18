<template>
    <div class="app-form">
        <div class="container-app-form">
            <h2>QUẢN LÝ ĐƠN TỪ</h2>
            <div class="header-app-form">
                <div class="detail-add">
                    <div class="action-app-form">
                        <MButton class="btn-cancel btn" :text="'Đơn xin nghỉ phép'" @click="showDayOff1"/>
                        <MButton class="btn-cancel btn" :text="'Đơn làm thêm giờ'" @click="showDayOff2"/>
                        <MButton class="btn-cancel btn" :text="'Đơn ứng lương'" @click="showSalary"/>    
                    </div>

                    
                </div>

                    
            </div>
            <div class="number-day">
                <div class="container-input">
                        <div  class="label">Số ngày nghỉ có phép</div>
                        <h2 class="input-detail">{{sal.dayoff}}</h2>

                    </div>
                    <div class="container-input">
                        <div  class="label">Số ngày làm thêm</div>
                        <h2 class="input-detail">{{hour}}</h2>

                    </div>
                    <div class="container-input">
                        <div  class="label">Số ngày phép được dùng</div>
                        <h2 class="input-detail">12</h2>

                    </div>
                    <div class="container-input">
                        <div  class="label">Số ngày phép còn lại</div>
                        <h2 class="input-detail">{{handleDay(sal.dayoff)}}</h2>

                    </div>
            </div>
           
            <div class="func" :hidden="role != 2">
                <div class="mine fuct" :class="{'selected-form': func == 1}" @click="changeFuc(1)">Đơn của tôi</div>
                <div class="them fuct" :class="{'selected-form': func == 2}" @click="changeFuc(2)">Phê duyệt đơn</div>
            </div>
            <div class="detail-app-form">
                <div class="container-table-app-form">
                <TheTableAppForm  :key ="keyTable" :employeeCode="code" :func="func" :name="nameUser"></TheTableAppForm>
                </div>
            </div>
            
        </div>
        
    </div>
    <MForm @closeForm="closeAppForm" v-if="isAppForm" :typeForm="typeAppForm"></MForm>

</template>

<script>
import MButton from '@/components/base/MButton.vue';
import TheTableAppForm from './TheTableAppForm.vue';
import MForm from '@/components/base/MForm.vue';

export default {
    components: { MButton, TheTableAppForm, MForm},
    created(){
        this.code = sessionStorage.getItem("code");
        this.role = sessionStorage.getItem("role");
        this.nameUser = sessionStorage.getItem("user");

        let url = `${this.Url}Salaries/code/` + this.code;
        var requestOptions = {
            method: 'GET',
            redirect: 'follow'
            };

        fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => {
            this.sal = result[0];
            
        })
    },
    methods:{
        handleDay(a){
            return 12 - a;
        },
        closeAppForm(){
            this.isAppForm = false;
        },
        showDayOff1(){
            this.typeAppForm = 0;
            this.isAppForm = true;
        },
        showDayOff2(){
            this.typeAppForm = 1;
            this.isAppForm = true;
        },
        showSalary(){
            this.typeAppForm = 2;
            this.isAppForm = true;
        },
        reload(){
            this.keyTable = Math.floor(Math.random()*90000);
        },
        changeFuc(e){
            this.func = e;
            this.reload();
        }
    },
    data(){
        return{
            isAppForm: false,
            typeAppForm: null,
            code: null,
            dayoff: 0,
            dayoff1:0,
            hour: 0,
            role: null,
            func: 1,
            keyTable: null,
            sal:{},
            Url: process.env.VUE_APP_URL,
        }
    }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../../css/layout/appForm.css);
</style>
  