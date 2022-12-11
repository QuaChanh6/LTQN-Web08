<template>
    <div class="container-button-oval">
        <div class="button-oval"  @click="showButton">
            <div class="button-text">{{text}}</div>
            <div class="button-oval-icon">
                <div class="button-icon-drop"></div>
            </div>
        </div>
        <div class="delete-button" v-show="isShowButton" @click="sendMsg" v-clickoutside="hideButton">{{type}}</div>
    </div>

</template>
  
<script>
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
    props:{
        text: String,
        listID: Array,
        isShowDelete: Boolean,
        type: String
    },
    directives: {
      clickoutside,
    },
    methods:{
        /**
         * Thực hiện xóa
         * author: LTQN(5/10/2022)
         */
        sendMsg(){
            this.$emit("deleteIDs", this.listID);
        },

        /**
         * Hiển thị nút xóa
         * author: LTQN(5/10/2022)
         */
        showButton(){
            if(this.isShowDelete){
                this.isShowButton =! this.isShowButton;
            }
        },

        /**
         * Ẩn nút xóa
         */
        hideButton(){
            if(this.isShowDelete){
                this.isShowButton = false;
            }
        }
    },
    data(){
        return{
            isShowButton: false
        }
    },
    watch:{
        isShowDelete(newVal){
            if(!newVal){
                this.isShowButton = false;
            }
        }
    }

  }
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/base/button.css);
</style>
  