<template>
    <div class="container-manage-user">
        <div class="title">QUẢN LÝ TÀI KHOẢN</div>
      <div class="container-table-user">
        <div class="search input-icon" style="margin-bottom: 12px;">
            <input type="text" class="input" placeholder="Tìm theo mã, tên nhân viên"  v-model="search" @keyup="searchEmployee">
            <div class="icon-search" @click="searchEmployee"></div>
        </div>
        <TheTableUser :key="keyReloadTable" :search='search' @reload="reload"></TheTableUser>
      </div>
    </div>
  </template>
  
  <script>
  import resource from '../../../common/resource';
import enumeration from '../../../common/enumeration';
  import TheTableUser from '../employee/TheTableUser.vue';
  export default {
    components: { TheTableUser },
    methods:{
        searchEmployee(){
            if(event.keyCode === enumeration.keyCode.ENTER || event.type === resource.event.Click){
                this.reload();
            }
        },
        reload(){
            this.keyReloadTable = Math.floor(Math.random()*90000);
        },
    },
    data(){
        return{
            search: '',
            keyReloadTable:null
        }
    }
  
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  
  </style>
  