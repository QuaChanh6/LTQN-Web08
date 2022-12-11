<template>
    <div class="container-department">
      <div class="content-header">
        <div class="title">DANH SÁCH PHÒNG BAN</div>
        <div class="tool-bar">
          <MButton class="add-department" :text= "'Thêm mới phòng ban'" @click='addDepartment'/>
        </div>
      </div>
      <div class="content-department-detail">
        <TheTableDepartment @showForm='showFormEdit' :key='keyLoad'/>
      </div>
    </div>
    <TheFormDepartment @closeForm='closeForm' v-if="isShowForm" @reload='reload'/>
  </template>
  
  <script>
import TheTableDepartment from './TheTableDepartment.vue';
import MButton from '@/components/base/MButton.vue';
import TheFormDepartment from './TheFormDepartment.vue';
  export default {
    components: { TheTableDepartment, MButton, TheFormDepartment },
    methods:{
      closeForm(){
        this.isShowForm = false;
      },
      addDepartment(){
        this.isShowForm = true;
      },
      showFormEdit(){
        this.isShowForm = true;
      },
      reload(){
        this.keyLoad = Math.floor(Math.random()*90000);
      }
    },
    data(){
      return{
        isShowForm: false,
        keyLoad: null
      }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/department/containerDepartment.css);
  </style>
  