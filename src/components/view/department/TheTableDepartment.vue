<template>
    <div class="container-table table-department">
        <table class="tableDep">
            <thead>
                <tr>
                    <th class="departmentCode">MÃ PHÒNG BAN</th>
                    <th class="departentName">TÊN PHÒNG BAN</th>
                    <th class="numberOfEmployee">SỐ LƯỢNG NHÂN VIÊN</th>
                    <th class="manager">QUẢN LÝ</th>
                </tr>
            </thead>           
            <tbody>
                <tr v-for="(dep, index) in deps" :key="index" v-show="hiddenDepartment[index]">
                    <td class="departmentCode">{{dep.departmentCode}}</td>
                    <td class="departentName">{{dep.departmentName}}</td>
                    <td class="numberOfEmployee">{{getCountOfDepartment(dep.departmentID)}}</td>
                    <td class="manager">{{dep.manager}}</td>
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
  export default {
    props:{
        hiddenDepartment: Array
    },
    created(){

        let url = process.env.VUE_APP_URL+'Departments';

        if(!format.checkEmptyData(localStorage.getItem('department'))){
            this.deps=JSON.parse(localStorage.getItem('department'));
            }
            else{//chưa có dữ liệu trong localstorage
                fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.deps = res;              
                    localStorage.setItem('department', JSON.stringify(res));
                }).catch(error => {
                    console.log(error);
            }) 
        }
        url= process.env.VUE_APP_URL+'Departments/countEmployee';
        fetch(url)
            .then(res => res.json())
            .then(res => {
                this.counts = res;              
            }).catch(error => {
                console.log(error);
        }) 

    },
    methods: {

        getCountOfDepartment(id){

            for(let count in this.counts){
                let Dep = this.counts[count];
                if(Dep.departmentID == id){
     
                    return Dep.count;
                }
            }

            return 0;
        }
    },
    data(){
        return{
            deps: [],
            counts: [],
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  .table-department{
    /* width: 800px; */
    height: 550px;

  }

  .tableDep{
    width: 100%;
    min-width: none;
  }
  .departmentCode{
    width: 100px;

  }

  .departentName{
    width: 150px;

  }

  .numberOfEmployee{
    width: 50px;

  }

  .manager{
    width: 100px;

  }
  </style>
  