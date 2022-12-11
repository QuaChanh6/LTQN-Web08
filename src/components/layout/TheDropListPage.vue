<template>
    <div class="dropList-page">
        <div class="dropList-text"  @click="isShow =! isShow">{{text}}</div>
        <div class="dropList-data" v-show="isShow">
            <div class="data-item" v-for="(textNumPage, index) in contentPage" :key="index" 
            @click="selectNumOfPage(textNumPage, index)"
            :class="{'dropList-choose': choose==index}">{{textNumPage.textData}}</div>
        </div>
        <div class="dropList-icon"  @click="isShow =! isShow">
            <div class="dropList-icon-drop"></div>
        </div>
    </div>
</template>
  
<script>
import resource from '@/common/resource';
  export default {
    props: {
      contentPage: Array // Mảng chứa lựa chọn số lượng bản ghi
    },
    created(){
      if(sessionStorage.getItem("numberRecordOfPage") != undefined){
        let obj = JSON.parse(sessionStorage.getItem("numberRecordOfPage"));
        this.text = obj.textData;
        this.$emit("numberRecordOfPage", obj.num);

      }
    },
    methods: {
      selectNumOfPage(textNumPage, index){
        this.choose = index;
        this.text = textNumPage.textData;
        this.$emit("numberRecordOfPage", textNumPage.num);
        this.isShow = false;
        //sessionStorage.removeItem('page');
        sessionStorage.setItem('page', 1);

        sessionStorage.setItem('numberRecordOfPage', JSON.stringify(textNumPage));

      }
    },
    data(){
      return{
        isShow: false,
        textNumPages: null,
        choose: 1,
        text: resource.textNumPages[1].textData
      }
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  