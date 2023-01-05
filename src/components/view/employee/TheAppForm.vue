<template>
    <div class="app-form">
        <div class="container-app-form">
            <h2>QUẢN LÝ ĐƠN TỪ</h2>
            <div class="header-app-form">
                <div class="detail-add">
                    <div class="action-app-form">
                        <MButton class="btn-cancel btn" :disablebutton="disablebutton" :text="'Đơn xin nghỉ phép'" @click="showDayOff1"/>
                        <MButton class="btn-cancel btn" :text="'Đơn làm thêm giờ'" @click="showDayOff2"/>
                        <MButton class="btn-cancel btn" :text="'Đơn ứng lương'" @click="showSalary"/>    
                    </div>

                    
                </div>

                    
            </div>
            <div class="number-day">
                <div class="container-input img-container">
                        <div  class="label">Số ngày nghỉ có phép</div>
                        <h2 class="input-detail">{{dayoff}}</h2>
                        <div class="img-a"></div>
                    </div>
                    <div class="container-input img-container">
                        <div  class="label">Số ngày làm thêm</div>
                        <h2 class="input-detail">{{sal.bonusDate}}</h2>
                        <div class="img-b"></div>

                    </div>
                    <div class="container-input img-container">
                        <div  class="label">Số ngày phép được dùng</div>
                        <h2 class="input-detail">12</h2>
                        <div class="img-c"></div>

                    </div>
                    <div class="container-input img-container">
                        <div  class="label">Số ngày phép còn lại</div>
                        <h2 class="input-detail">{{handleDay(dayoff)}}</h2>
                        <div class="img-d"></div>

                    </div>
            </div>
           
            <div class="func" v-if="role == 2">
                <div class="mine fuct" :class="{'selected-form': func == 1}" @click="changeFuc(1)">Đơn của tôi</div>
                <div class="them fuct" :class="{'selected-form': func == 2}" @click="changeFuc(2)">Phê duyệt đơn</div>
            </div>
            <div class="detail-app-form">
                <div class="container-table-app-form" style="margin-top: 16px;">
                <TheTableAppForm  :key ="keyTable" :employeeCode="code" :func="func" :name="nameUser"></TheTableAppForm>
                </div>
            </div>
            
        </div>
        
    </div>
    <MForm @closeForm="closeAppForm" v-if="isAppForm" :typeForm="typeAppForm" @reload="reload"></MForm>

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

        let now = new Date();
        let day = (now.getMonth() + 1).toString() + "N" + now.getFullYear().toString();
        if(day.length == 6) day = "0"+day;
        let url = process.env.VUE_APP_URL + "Salaries/code/" + this.code + "/" + day;
        var requestOptions = {
            method: 'GET',
            redirect: 'follow'
            };

        fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => {
            this.sal = result[0];
            url = process.env.VUE_APP_URL + "Salaries/dayoff/" + this.code;
            fetch(url, requestOptions)
            .then(response => response.json())
            .then(result => {
                this.dayoff = result[0];
                
            })
            
        })
    },
    methods:{
        handleDay(a){
            let day = 12 -a;
            if(day == 0){
                this.disablebutton = true;
            }else{
                this.disablebutton = false;
            }
            return day;
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
            dayoff: null,
            dayoff1:0,
            hour: 0,
            role: null,
            func: 1,
            keyTable: null,
            sal:{},
            Url: process.env.VUE_APP_URL,
            disablebutton: false
        }
    }

}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../../css/layout/appForm.css);
</style>
  