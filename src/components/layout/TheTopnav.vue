<template>
    <div class="top-nav"  @mousemove="getPosition" @mouseleave="hideTooltip">
            <div class="top-nav__left">
                <div class="option-icon"></div>
                <div class="option-company"  @mouseenter="showTooltip('Công ty Trách nhiệm Hữu hạn Sản xuất-Thương mại-Dịch vụ')">
                  CÔNG TY TNHH SẢN XUẤT-THƯƠNG MẠI-DỊCH VỤ</div>
                <div class="arrow-down-icon"></div>
            </div>
            <div class="top-nav__right" @click="showLogOut">
                <div class="avatar"></div>
                <div class="name">{{nameOfUser}}</div>
                <div class="arrow-down-icon-emp"></div>
            </div>

        </div>
<TheLogOut @hideLogOut="hideLogOut" v-show="isShowLogOut" @logOut="logOut"></TheLogOut>
</template>
  
<script>
import TheLogOut from "./TheLogOut.vue"
  export default {
    props:{
      nameOfUser: String
    },

    methods: {
        /**
          * Lấy vị trí con chuột
          * author: LTQN(2/10/2022)
          * @param {*} event
          */
        getPosition(event) {
            this.posX = event.pageX;
            this.posY = 45;
        },
        /**
         * Hiển thị tooltip
         * author: LTQN(2/10/2022)
         * @param {string} content : nội dung
         */
        showTooltip(content) {
            this.$emit("showTooltip", { contentTooltip: content, positionX: this.posX, positionY: this.posY });
        },
        /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip() {
            this.$emit("hideTooltip");
        },
        hideLogOut(){
          this.isShowLogOut = false;
        },
        showLogOut(){
          this.isShowLogOut = !this.isShowLogOut;
        },
        logOut(){
          sessionStorage.removeItem('user');
          this.$emit('userLogOut');
        }
       
    },
    data() {
        return {
            posX: null,
            posY: null,
            contentOfToastMessage: "",
            isShowToast: false,
            typeToast: "",
            isShowLogOut: false
        };
    },
    components: { TheLogOut}
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/layout/topnav.css);
</style>
  