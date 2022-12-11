<template>
        <!-- <combobox id="position" api="https://cukcuk.manhnv.net/api/v1/Positions" text="PositionName" value="PositionId"></combobox> -->
    <div class="combobox-container">
          <div class="input-container"   :class="{'hoverEmptyDepartment': emptyCombobox}">
              <input type="text" class="combobox input" :placeholder="placeholder" 
              @keyup="searchDataCombobox" @focus="comboboxFocus" ref="Department"
              v-model="dataSelected"  :class="{'border-red': emptyCombobox}">
              <div class="not-empty emptyCombobox">Không được để trống.</div>
              <div id ="icon-combobox" class="icon-combobox"  @click="showData" >
                  <div class="icon-drop-combobox"></div>
              </div>
          </div>
          <!-- <div class="active">{{dataSelected}}</div> -->
          <div class="combobox-value" v-if="showDataList" v-clickoutside="hideListData" ref="comboboxValue">
              <div class="combobox-item" v-for="(item, index) in items" :key="index" 
              @click="selectedDataCombobox(item, index)" 
              :class="{'combobox-item-active': selected == index}"
              v-show="isSearch[index]"
              @keydown="directItemDownUp"
              tabindex="0" >
              <span>{{item[this.code]}}</span>
              {{item[this.name]}}
                <div class="icon-tick" v-show="selected==index"></div></div>
          </div>
      </div>
  </template>
  
<script>
import format from '@/common/formatData';
import enumeration from '@/common/enumeration';

/**
 * Click ra ngoài thì ẩn
 * author: LTQN(8/10/2022)
 */
const clickoutside = {
  mounted(el, binding) {
    el.clickOutsideEvent = (event) => {
      //click ra ngoài
      // event: đối tượng click
      if (
        !(( // click phạm vi của combobox-value || click vào ele trước combobox-value
            el === event.target || el.previousElementSibling.contains(event.target)
          ))
      ) {
        // thực hiện giá trị truyền vào
        binding.value();
      }
    };
    document.body.addEventListener("click", el.clickOutsideEvent);
  },
  beforeUnmount: (el) => {
    document.body.removeEventListener("click", el.clickOutsideEvent);
  },
};

  export default {
    created(){
      try {
        //render data khi ấn nút sửa
        this.dataSelected = this.valueRender.name;

      } catch (error) {
        console.log(error)
      }
 
    },
    directives: {
      clickoutside,
    },
    watch: {
        //ban đầu dataList bị ẩn nên data chưa được render ra
        //mà chức năng search sẽ ảnh hưởng đến việc data hiển thị trong lần đầu
        showDataList(){
          this.items.forEach((item, index) => {
            this.isSearch[index] = true;
          })
        },
        selected(newVal){
          if(this.showDataList){
            this.$nextTick(() => {
              this.setScroll(newVal);
            });
          }
        },
        items(newVal, oldVal){
          if(oldVal.length == 0){
            newVal.forEach((item, index) => {
              if(item[this.name] == this.valueRender.name){
                  this.selected = index;
              }
            })
          }
        },
    
        focus(){
          if(this.emptyCombobox){
            this.$refs['Department'].focus();
          }
        }
    },
    methods: {
      /**
       * Set vị trí thanh cuộn
       * author: LTQN(7/10/2022)
       * @param {int} selected : vị trí phần tử được chọn
       */
      setScroll(selected){
        this.$refs.comboboxValue.scrollTop = selected * 36;
      },
      /**
       * Hàm chọn dữ liệu
       * author: LTQN(10/9/2022)
       * @param {*} item : data trong combobox
       * @param {*} index : vị trí data
       */
      selectedDataCombobox(item, index){
        try {
          this.selected = index;
          this.dataSelected = item[this.name]; //text: Department
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
          if(this.selected === null) this.selected = -1;
          let key = event.keyCode;
          let selectedCurrent = this.selected;
          switch(key){
            case enumeration.keyCode.DOWN:
              // this.$refs[`focus_${this.selectedCurrent + 1}`].click();
              if(selectedCurrent < this.items.length-1){
                selectedCurrent++;
                while(!this.isSearch[selectedCurrent]){ //nếu bị ẩn
                  selectedCurrent++;
                  if(selectedCurrent === this.items.length){
                    selectedCurrent = this.selected;
                    break;
                  }
                }
              }   
              this.selected = selectedCurrent;
              this.dataSelected = this.items[this.selected][this.name];
              break;
            case enumeration.keyCode.UP:
              if(selectedCurrent > 0){
                selectedCurrent--;
                while(!this.isSearch[selectedCurrent]){ //nếu bị ẩn
                  selectedCurrent--;
                  if(selectedCurrent === -1){
                    selectedCurrent = this.selected;
                    break;
                  }
                }
              }  
              this.selected = selectedCurrent;
              this.dataSelected = this.items[this.selected][this.name];
              break;
            case enumeration.keyCode.ENTER:
              this.dataSelected = this.items[this.selected][this.name];
              this.$emit('objectItemCombobox', this.items[this.selected]);
              this.showDataList = false;
              //reset lại
              //this.selected = -1;
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
      searchDataCombobox(event){
        try {
          if(event.keyCode != enumeration.keyCode.TAB){
            this.dataList();
            this.showDataList = true;
            if(!format.checkEmptyData(this.dataSelected)){
              let textInput = format.processData(this.dataSelected);
              this.items.forEach((item, index) => {
                  this.isSearch[index] = false;
                    let temp = format.processData(item[this.name]);
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
          
          }
        
        } catch (error) {
          console.log(error)
        } 
      },
      showData(){
        this.showDataList =! this.showDataList;
        if(this.showDataList === true){
          this.dataList();
          if(this.selected != null){
            this.$nextTick(() => {
              this.setScroll(this.selected);
            });
          }
        }
      },

      /**
       * Hàm hiển thị dữ liệu combobox
       * author: LTQN(27/9/2022)
       */
      dataList(){    
        try {
            if(!format.checkEmptyData(localStorage.getItem(this.textName))){
            this.items=JSON.parse(localStorage.getItem(this.textName));
            }
            else{//chưa có dữ liệu trong localstorage
                fetch(this.url)
                .then(res => res.json())
                .then(res => {
                    this.items = res;              
                    localStorage.setItem(this.textName, JSON.stringify(this.items));
                    // this.showDataList =! this.showDataList;
                }).catch(error => {
                  console.log(error);
                }) 
            }

 
        } catch (error) {
          console.log(error);
        }
      
      },

      /**
       * Hàm gửi khi focus vào input
       * author: LTQN(6/10/2022)
       */
      comboboxFocus(){
        this.$emit('comboboxFocus');
      },

      /**
       * Ẩn comboboxvalue
       * author: LTQN(10/9/2022)
       */
      hideListData(){
        this.showDataList=false;
      },



    },
    props: {
      url: String,
      textName: String, //Department
      valueRender: Object,//ID render lên combobox
      placeholder: String,
      emptyCombobox: Boolean,
      focus: Boolean, //tự động focus vào hàm sai
    },
    data(){
        return{
          items: [],
          showDataList: false,
          selected: null, // làm nổi bật cái được chọn
          dataSelected: '', //text: dữ liệu được chọn
          textSearch: '',
          isSearch: [],
          name:  `${this.textName}Name`,
          code: `${this.textName}Code`,
        }
    },


  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/combobox.css);

</style>
  