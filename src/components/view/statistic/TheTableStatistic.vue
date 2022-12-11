<template>
    <div class="container-table">
        <table>
            <thead>
                <tr>
                    <th class="employeeCode">MÃ NHÂN VIÊN</th>
                    <th class="employeeName">TÊN NHÂN VIÊN</th>
                    <th class="salary">LƯƠNG CƠ BẢN</th>
                    <th class="numWork">SỐ NGÀY CÔNG</th>
                    <th class="numWork">SỐ NGÀY LÀM THÊM</th>
                    <th class="bonus">LƯƠNG THƯỞNG</th>
                    <th class="advance-money">ỨNG TRƯỚC</th>
                    <th class="total-money">TỔNG LƯƠNG</th>
                    <th class="accountBank">SỐ TÀI KHOẢN</th>
                    <th class="nameOfBank">TÊN NGÂN HÀNG</th>
                    <th class="branchOfBank">CHI NHÁNH TÀI KHOẢN NGÂN HÀNG</th>
                    <th class="sticky-col header-sticky-col">CHỨC NĂNG</th>
                </tr>
            </thead>           
            <tbody>
                <tr @dblclick="showForm(sal)" v-for="(sal, index) in salaries" :key="index" >
                    <td class="employeeCode">{{sal.salaryCode}}</td>
                    <td class="employeeName">{{sal.employeeName}}</td>
                    <td class="salary">{{formatMoney(sal.salaryMonthly)}}</td>
                    <td class="numWork">{{sal.numberWork}}</td>
                    <td class="bonusDate">{{sal.bonusDate}}</td>
                    <td class="bonus">{{formatMoney(sal.bonus)}}</td>
                    <td class="advance-money">{{formatMoney(sal.advanceMoney)}}</td>
                    <td class="total-money">{{formatMoney(formatTotal(sal.salaryMonthly,sal.numberWork,sal.bonusDate, sal.bonus, sal.advanceMoney))}}</td>
                    <td class="accountBank">{{sal.accountBank}}</td>
                    <td class="nameOfBank">{{sal.nameOfBank}}</td>
                    <td class="branchOfBank">{{sal.branchOfBank}}</td>
                    <td class="sticky-col" @click="showForm(sal)">Sửa</td>
                </tr>  
                            
            </tbody>
        </table>
        <!-- <div class="loadingTable" v-show="isLoadTable"><div v-for="i in 10" :key="i" class="container-skeleton">
            <div  class="skeleton" v-for="i in 5" :key="i"></div>
        </div> -->
    </div>

  </template>
  
  <script>
import format from '@/common/formatData';
    async function getUserAsync(url) {
        try {
            let response = await fetch(url);
            let data = await response.json();
            return data;
        } catch (error) {   
            console.log(error);
            return [];
        }
        
    }

  export default {
    props:{
        search:String
    },
    created(){
        try {
            let url = process.env.VUE_APP_URL+'Salaries';
            if(this.search !=  ''){
               url = url + "?keyword="+this.search;
            }

            getUserAsync(url).then(data =>{
                this.isLoadTable = false;
                this.salaries = data;

            });


        } catch (error) {
            console.log(error);
        }
    },
    methods: {
        formatTotal(salaryMonthly,NumberWork,bonusDate, bonus, advanceMoney){
            let tong = Math.round((salaryMonthly/22).toFixed(0) * (NumberWork + bonusDate + bonusDate*0.5)) + bonus - advanceMoney;
            return tong;
        },
        //hiển thị form
        showForm(salary){
            this.$emit('showForm', salary);
        },
        formatMoney(money){
            return format.formatMoney(money);
        }
    },
    data(){
        return{
            salaries: [],
            chooseTr: null,
            salary: {},
            isLoadTable: false, // load table
        }
    },

  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  table{
    min-width: 1900px;
  }

  .sticky-col{
    cursor: pointer;
  }
  </style>
  