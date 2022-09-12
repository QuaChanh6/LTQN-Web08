<template>
    <div class="dropList--v2">
        <div class="dropList__text">{{text}}</div>
        <div class="dropList__data" v-show="isShow">
            <div class="data-item" v-for="(textNumPage, index) in textNumPages" :key="index" 
            @click="selectNumOfPage(textNumPage, index)"
            :class="{'dropList-choose': choose==index}">{{textNumPage.textData}}</div>
            <!-- <div class="data-item" @click="selectNumOfPage('10')" :class="{'dropList-choose': choose==numPage['10']}">{{textNumPage['10']}}</div>
            <div class="data-item" @click="selectNumOfPage('20')" :class="{'dropList-choose': choose==numPage['20']}">{{textNumPage['20']}}</div>
            <div class="data-item" @click="selectNumOfPage('30')" :class="{'dropList-choose': choose==numPage['30']}">{{textNumPage['30']}}</div>
            <div class="data-item" @click="selectNumOfPage('50')" :class="{'dropList-choose': choose==numPage['50']}">{{textNumPage['50']}}</div>
            <div class="data-item" @click="selectNumOfPage('100')" :class="{'dropList-choose': choose==numPage['100']}">{{textNumPage['100']}}</div> -->
        </div>
        <div class="dropList__icon" @click="isShow =! isShow">
            <div class="dropList_icon-drop"></div>
        </div>
    </div>
</template>
  
<script>
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
        // numPage: {'10': '10', '20': '20', '30': '30', '50': '50', '100': '100'},
        textNumPages: [
          {
            num: '10',
            textData: '10 bản ghi trên một trang'
          },
          {
            num: '20',
            textData: '20 bản ghi trên một trang'
          },
          {
            num: '30',
            textData: '30 bản ghi trên một trang'
          },
          {
            num: '50',
            textData: '50 bản ghi trên một trang'
          },
          {
            num: '100',
            textData: '100 bản ghi trên một trang'
          },
        ],
      
          // '10': '10 bản ghi trên một trang',
          // '20': '20 bản ghi trên một trang',
          // '30': '30 bản ghi trên một trang',
          // '50': '50 bản ghi trên một trang',
          // '100': '100 bản ghi trên một trang'

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
  