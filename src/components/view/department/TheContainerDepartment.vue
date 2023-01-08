<template>
    <div class="container-department">
      <div class="content-header">
        <div class="title">DANH SÁCH PHÒNG BAN</div>
        <div class="tool-bar">
          <MButton class="add-department" :text= "'Thêm mới phòng ban'" @click='addDepartment'/>
        </div>
      </div>
      <div class="content-department-detail">
        <div class="search input-icon" style="margin-bottom: 12px;margin-left: 12px;">
            <input type="text" class="input" placeholder="Tìm kiếm phòng ban"  v-model="search" @keyup="searchDepartment">
            <div class="icon-search" @click="searchDepartment"></div>
        </div>
        <TheTableDepartment @showForm='showFormEdit' :key='keyLoad' :hiddenDepartment="hiddenDepartment"/>
      </div>
    </div>
    <TheFormDepartment @closeForm='closeForm' v-if="isShowForm" @reload='reload'/>
  </template>
  
  <script>
import TheTableDepartment from './TheTableDepartment.vue';
import MButton from '@/components/base/MButton.vue';
import TheFormDepartment from './TheFormDepartment.vue';
import resource from '@/common/resource'
import enumeration from '@/common/enumeration';
import format from '@/common/formatData';
  export default {
    components: { TheTableDepartment, MButton, TheFormDepartment },
    created(){
      this.departments=JSON.parse(localStorage.getItem('department'));
      for(let i=0;i<this.departments.length;i++){
        this.hiddenDepartment[i] = true;
      }
    },
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
      },
      searchDepartment(event){
        if(event.keyCode === enumeration.keyCode.ENTER || event.type === resource.event.Click){
          if(format.checkEmptyData(this.search)){
            for(let i=0;i<this.departments.length;i++){
              this.hiddenDepartment[i] = true;
            }
          }else{
            for(let i=0;i<this.departments.length;i++){
            let textInput = format.processData(this.departments[i].departmentName);
            let textSearch = format.processData(this.search);
            if(textInput.search(textSearch) == -1){ // nếu không chứa chuỗi
              this.hiddenDepartment[i] = false;
            }
           
          }
          }
        
        }
      },
    },
    data(){
      return{
        isShowForm: false,
        keyLoad: null,
        hiddenDepartment: [],
        departments: [],
        search: ''
      }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/department/containerDepartment.css);
  </style>
  