<template>
    <div class="app-form-table">
        <table style="width:100%;">
            <thead>
                <tr>
                    <th class="type af">LOẠI ĐƠN</th>
                    <th class="sDate af">TỪ NGÀY</th>
                    <th class="eDate af">ĐẾN NGÀY</th>
                    <th class="numDate af" v-if="func!=2">TỔNG NGÀY</th>
                    <th class="numDate af" v-if="func==2">NHÂN VIÊN</th>
                    <th class="reason af">LÝ DO</th>
                    <th class="type af" >NGÀY TẠO</th>
                    <th class="sal af">ỨNG TIỀN (nếu có)</th>
                    <th class="statusApp af">TÌNH TRẠNG</th>
                </tr>
            </thead>           
            <tbody ref="bodyTable">
                <tr v-for="(app, index) in appform0" :key="index">
                    <td class="type">{{handleType(app.type)}}</td>
                    <td class="sDate">{{formatDate(app.dateStart)}}</td>
                    <td class="eDate">{{formatDate(app.dateEnd)}}</td>
                    <td class="numDate" v-if="func!=2">{{handleNumberDate(app.dateStart, app.dateEnd)}}</td>
                    <td class="emloyee" v-if="func==2">{{app.employeeName}}</td>
                    <td class="reason">{{app.reason}}</td>
                    <td class="type" >{{formatDate(app.createdDate)}}</td>
                    <td class="type" >{{app.money}}</td>
                    <td class="statusApp">{{handleStatus(app.status)}}
                    <span :hidden="func != 2 || app.status != 0" class="approve app-x" @click="approve(0, app)">x</span>
                    <span :hidden="func != 2 || app.status != 0" class="approve app-v" @click="approve(1, app)">v</span>
                    </td>
                </tr>  
                            
            </tbody>
        </table>
    </div>
  
   

</template>

<script>
import format from '@/common/formatData';



export default {
    props:{
        employeeCode: String,
        func : Number,
        name: String
    },
    created() {
        var requestOptions = {
        method: 'GET',
        redirect: 'follow'
        };
        let url = "https://localhost:7271/api/v1/AppForms/filtermanager?name=" + this.name;
        if(this.func == 1){
            url = "https://localhost:7271/api/v1/AppForms/code/"+ this.employeeCode
        }
        fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => {
            // Object.assign(this.appform0, result.filter(x => x.type != 2));
            // Object.assign(this.appform1, result.filter(x => x.type == 2));
            Object.assign(this.appform0, result);

            console.log(result);
        })
        .catch(error => console.log('error', error));
    
    },
    methods:{
          /**
         * Hàm format date ngày/tháng/năm
         * author: (14/9/2022)
         * @param {String} date 
         */
         format(date){
            return format.formatDate(date);
        },
        formatDate(e){
            if(!format.checkEmptyData(e)){
                return format.formatDate(e);
            }
        },
        closeForm(){
            this.$emit('closeForm');
        },
        handleNumberDate(start, end){
            return format.distanceDate(start, end);
        },
        handleType(e){
            switch(e){
                case 1:
                    return "Làm thêm giờ";
                case 0:
                    return "Nghỉ phép";
                case 2:
                    return "Ứng lương";
            }
        },
        handleStatus(e){
            switch(e){
                case 0:
                    return "Đang chờ duyệt";
                case 1:
                    return "Đã duyệt";
                case 2:
                    return "Đã từ chối";
            }
        },
        approve(e, app){
            switch(e){
                case 0: // từ chối
                    app.status = 2;
                    this.updateAppForm(app);
                break;
                case 1: // đồng ý
                    app.status = 1;
                    this.updateAppForm(app);
                break;
            }
        },
        async updateAppForm(appform){
            let url= `${this.Url}AppForms/` + appform.id;
            await fetch(url, {
            method: 'PUT',
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            body: JSON.stringify(appform),
            }) 
            if(appform.status == 1){ // nếu đồng ý
                url = `${this.Url}Salaries/code/` + appform.employeeCode;

                var requestOptions = {
                    method: 'GET',
                    redirect: 'follow'
                    };

                fetch(url, requestOptions)
                .then(response => response.json())
                .then(result => {
                    this.sal = result[0];
                    if(appform.type == 0){
                        this.sal.dayoff = this.sal.dayoff + Number(this.handleNumberDate(appform.dateStart, appform.dateEnd));
                    }
                    if(appform.type == 1){
                        this.sal.bonusDate = this.sal.bonusDate + Number(this.handleNumberDate(appform.dateStart, appform.dateEnd));
                    }
                    if(appform.type == 2){
                        this.sal.advanceMoney = this.sal.advanceMoney + appform.money;
                    }
                    this.editSalary();
                })
            }
        },
        async editSalary(){
            let url= process.env.VUE_APP_URL +"Salaries/" + this.sal.salaryID;
            await fetch(url, {
            method: 'PUT',
            headers: {'Accept': 'application/json','Content-Type': 'application/json'},
            body: JSON.stringify(this.sal),
            })
    
        }
    },
    data(){
        return{
            appform0: [], //nghỉ phép/làm thêm giờ
            appform1: [], // ứng lương
            isShow: false,
            status : '',
            Url: process.env.VUE_APP_URL,
            sal: {}
        }
    }
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url("../../../css/layout/tableappform.css");

</style>
  