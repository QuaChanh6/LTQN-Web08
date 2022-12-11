<template>
     <div class="container-position">
      <div class="content-header">
        <div class="title">DANH SÁCH CHỨC VỤ </div>
        <div class="tool-bar">
          <MButton class="add-position" :text= "'Thêm mới chức vụ'" @click='addPosition'/>
        </div>
      </div>
      <div class="content-position-detail">
       <TheTablePossition @showForm='showFormEdit' :key='keyLoad'/>
      </div>
    </div>
    <TheFormPosition @closeForm='closeForm' v-if='isShowForm' @reload='reload'/>
  </template>
  
  <script>
import TheTablePossition from "./TheTablePossition.vue";
import MButton from "@/components/base/MButton.vue";
import TheFormPosition from "./TheFormPosition.vue";
  export default {
    components: { TheTablePossition, MButton, TheFormPosition },
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
  @import url(../../../css/position/containerPosition.css);
  </style>
  