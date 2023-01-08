<template>
     <div class="container-position">
      <div class="content-header">
        <div class="title">DANH SÁCH CHỨC VỤ </div>
        <div class="tool-bar">
          <MButton class="add-position" :text= "'Thêm mới chức vụ'" @click='addPosition'/>
        </div>
      </div>
      <div class="content-position-detail">
        <div class="search input-icon" style="margin-bottom: 12px;margin-left: 12px;">
            <input type="text" class="input" placeholder="Tìm kiếm chức vụ"  v-model="search" @keyup="searchPosition">
            <div class="icon-search" @click="searchPosition"></div>
        </div>
       <TheTablePossition @showForm='showFormEdit' :key='keyLoad' :hiddenPosition="hiddenPosition"/>
      </div>
    </div>
    <TheFormPosition @closeForm='closeForm' v-if='isShowForm' @reload='reload'/>
  </template>
  
  <script>
import TheTablePossition from "./TheTablePossition.vue";
import MButton from "@/components/base/MButton.vue";
import TheFormPosition from "./TheFormPosition.vue";
import resource from '@/common/resource'
import enumeration from '@/common/enumeration';
import format from '@/common/formatData';

  export default {
    components: { TheTablePossition, MButton, TheFormPosition },
    created(){
      this.positions=JSON.parse(localStorage.getItem('position'));
      for(let i=0;i<this.positions.length;i++){
        this.hiddenPosition[i] = true;
      }
    },
    methods:{
      closeForm(){
        this.isShowForm = false;
      },
      addPosition(){
        this.isShowForm = true;
      },
      showFormEdit(){
        this.isShowForm = true;
      },
      reload(){
        this.keyLoad = Math.floor(Math.random()*90000);
      },
      searchPosition(event){
        if(event.keyCode === enumeration.keyCode.ENTER || event.type === resource.event.Click){
          if(format.checkEmptyData(this.search)){
            for(let i=0;i<this.positions.length;i++){
              this.hiddenPosition[i] = true;
            }
          }else{
            for(let i=0;i<this.positions.length;i++){
            let textInput = format.processData(this.positions[i].positionName);
            let textSearch = format.processData(this.search);
            if(textInput.search(textSearch) == -1){ // nếu không chứa chuỗi
              this.hiddenPosition[i] = false;
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
        hiddenPosition: [],
        search: '',
        positions: []
      }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/position/containerPosition.css);
  </style>
  