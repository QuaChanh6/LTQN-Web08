<template>
    <div class="dropList-page">
        <div class="dropList-text">{{text}}</div>
        <div class="dropList-data" v-show="isShow">
            <div class="data-item" v-for="(textNumPage, index) in contentPage" :key="index" 
            @click="selectNumOfPage(textNumPage, index)"
            :class="{'dropList-choose': choose==index}">{{textNumPage.textData}}</div>
        </div>
        <div class="dropList-icon" @click="isShow =! isShow">
            <div class="dropList-icon-drop"></div>
        </div>
    </div>
</template>
  
<script>

  export default {
    props: {
      contentPage: Array // Mảng chứa lựa chọn số lượng bản ghi
    },
    methods: {
      selectNumOfPage(textNumPage, index){
        this.choose = index;
        this.text = textNumPage.textData;
        this.$emit("numberRecordOfPage", textNumPage.num);
        this.isShow = false;
      }
    },
    data(){
      return{
        isShow: false,
        textNumPages: null,

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
  