<template>
    <div class="dropList-page">
        <div class="dropList-text">{{text}}</div>
        <div class="dropList-data" v-show="isShow">
            <div class="data-item" v-for="(textNumPage, index) in textNumPages" :key="index" 
            @click="selectNumOfPage(textNumPage, index)"
            :class="{'dropList-choose': choose==index}">{{textNumPage.textData}}</div>
        </div>
        <div class="dropList-icon" @click="isShow =! isShow">
            <div class="dropList-icon-drop"></div>
        </div>
    </div>
</template>
  
<script>
import Resource from '@/common/Resource';
  export default {
    methods: {
      selectNumOfPage(textNumPage, index){
        this.choose = index;
        this.text = textNumPage.textData;
        this.isShow = false;
      }
    },
    data(){
      return{
        isShow: false,
        textNumPages: Resource.textNumPages,

        choose: null,
        text: '-- Tùy chọn --'
      }
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  