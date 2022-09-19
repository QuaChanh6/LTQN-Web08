<template>
        <!-- <combobox id="position" api="https://cukcuk.manhnv.net/api/v1/Positions" text="PositionName" value="PositionId"></combobox> -->
    <div class="combobox-container">
          <div class="input-container">
              <input type="text" class="combobox input" placeholder="Đơn vị" 
              @keyup="searchDataCombobox" 
              v-model="dataSelected" 
              :ref="'input'">
              <div id ="icon-combobox" class="icon-combobox"  @click="showDataList=!showDataList" >
                  <div class="icon-drop-combobox"></div>
              </div>
          </div>
          <div class="combobox-value" v-if="showDataList" :ref="'dataList'">
              <div class="combobox-item" v-for="(item, index) in items" :key="index" 
              @click="selectedDataCombobox(item, index)" 
              :class="{'combobox-item-active': selected == index}"
              v-show="isSearch[index]"
              :ref="'focus_' +index"
              @keydown="directItemDownUp"
              tabindex="0" >
              {{item[this.text]}}
                <div class="icon-tick" v-show="selected==index"></div></div>
          </div>
      </div>
  </template>
  
<script>

import format from '@/common/formatData';
import Enumeration from '@/common/Enumeration';
  export default {
    created(){
      fetch(this.url)
        .then(res => res.json())
        .then(res => {
            this.items=res;
        })

        //render data khi ấn nút sửa
        this.dataSelected = this.valueRender;

    },
    beforeUpdate(){
      
    },
    watch: {
        //ban đầu dataList bị ẩn nên data chưa được render ra
        //mà chức năng search sẽ ảnh hưởng đến việc data hiển thị trong lần đầu
        showDataList(){
          this.items.forEach((item, index) => {
            this.isSearch[index] = true;
          })
        }

    },
    methods: {
      /**
       * Hàm chọn dữ liệu
       * author: LTQN(10/9/2022)
       */

      /**
       * 
       * @param {*} item : data trong combobox
       * @param {*} index : vị trí data
       */
      selectedDataCombobox(item, index){
        try {
          this.selected = index;
          this.dataSelected = item[this.text]; //text: Department
          this.showDataList = false;
          //gửi đối tượng được chọn
          this.$emit('objectItemCombobox', item);
        } catch (error) {
          console.log(error)
        }
         
          
      },
      /**
       * Hàm điều hướng lên xuống
       * author: LTQN(10/9/2022)
       */
      directItemDownUp(event){
        try {
          if(this.selected == null) this.selected = -1;
          let key = event.keyCode;
          let selectedCurrent = this.selected;
          switch(key){
            case Enumeration.keyCode.DOWN:
              // this.$refs[`focus_${this.selectedCurrent + 1}`].click();
              if(selectedCurrent < this.items.length-1){
                selectedCurrent++;
                while(!this.isSearch[selectedCurrent]){ //nếu bị ẩn
                  selectedCurrent++;
                  if(selectedCurrent == this.items.length){
                    selectedCurrent = this.selected;
                    break;
                  }
                }
              }   
              this.selected = selectedCurrent;
              break;
            case Enumeration.keyCode.UP:
              if(selectedCurrent > 0){
                selectedCurrent--;
                while(!this.isSearch[selectedCurrent]){ //nếu bị ẩn
                  selectedCurrent--;
                  if(selectedCurrent == -1){
                    selectedCurrent = this.selected;
                    break;
                  }
                }
              }  
              this.selected = selectedCurrent;
              break;
            case Enumeration.keyCode.ENTER:
              this.dataSelected = this.items[this.selected][this.text];
              this.$emit('objectItemCombobox', this.items[this.selected]);
              this.showDataList = false;
              //reset lại
              this.selected = -1;
              break;
            default:
              break;
            }
        } catch (error) {
          console.log(error)
        }
       
      },
        /**
       * Hàm tìm kiếm
       * author: LTQN(10/9/2022)
       */
      searchDataCombobox(){
        try {
          this.showDataList = true;
          if(this.dataSelected != '' || this.dataSelected == undefined){
            let textInput = format.processData(this.dataSelected);
            this.items.forEach((item, index) => {
                this.isSearch[index] = false;
                  let temp = format.processData(item[this.text]);
                  // console.log(this.$refs[`show_${index}`]);
                  // console.log(item);
                  if(temp.search(textInput) != -1){ // nếu có chứa chuỗi
                      this.isSearch[index] = true;
                  
                }
          })
          }else{ //nếu rỗng thì hiển thị tất cả
            this.items.forEach((item, index) => {
              this.isSearch[index] = true;
            })
            this.selected= null;
            //gửi về 1 object rỗng
            this.$emit("objectItemCombobox", {});
          }
        } catch (error) {
          console.log(error)
        } 
      }
       
    },
    props: {
      url: String,
      text: String, //DepartmentID
      valueRender: String//ID render lên combobox
    },
    data(){
        return{
          items: [],
          showDataList: false,
          selected: null, // làm nổi bật cái được chọn
          dataSelected: '', //text: dữ liệu được chọn
          textSearch: '',
          isSearch: [],
        }
    },
  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/combobox.css);

</style>
  