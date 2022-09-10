<template>
        <!-- <combobox id="position" api="https://cukcuk.manhnv.net/api/v1/Positions" text="PositionName" value="PositionId"></combobox> -->
    <div class="combobox-container">
          <div class="input-container">
              <input type="text" class="combobox input" placeholder="Đơn vị" :value="dataSelected">
              <div id ="icon-combobox" class="icon-combobox">
                  <div class="icon-drop-combobox" @click="showDataList=!showDataList"></div>
              </div>
          </div>
          <div class="combobox-value" v-show="showDataList">
              <!-- <div class="combobox-item combobox-item-active" value="0">Nữ<div class="icon-tick"></div></div> -->
              <div class="combobox-item" v-for="(item, index) in items" :key="index" 
              @click="selectedDataCombobox(item, index)" 
              :class="{'combobox-item-active': selected == index}">
              {{item[this.text]}}
                <div class="icon-tick" v-show="selected==index"></div></div>
              <!-- <div class="combobox-item" value="2">Khác</div> -->
          </div>
      </div>
  </template>
  
<script>

  export default {
    created(){
      fetch(this.url)
        .then(res => res.json())
        .then(res => {
            this.items=res;
        })
    },
    methods: {
      selectedDataCombobox(item, index){
          this.selected = index;
          this.dataSelected = item[this.text];
          this.showDataList = false;
          //gửi đối tượng được chọn
          this.$emit('objectItemCombobox', item);
      }
    },
    props: {
      url: String,
      text: String,
    },
    data(){
        return{
          items: [],
          showDataList: false,
          selected: null,
          dataSelected: ''
        }
    }
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/combobox.css);
</style>
  