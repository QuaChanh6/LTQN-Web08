<template>
    <div class="side-bar" @mousemove="getPosition" @mouseleave="hideTooltip">
        <div class="side-bar-title">
            <div class="select-option-icon"></div>
            <div class="title-bar"> QUẢN LÝ HỒ SƠ</div>
        </div>
        <div class="side-bar-list">
            <div class="side-bar-item">
                <div class="icon-total side-bar-icon"></div>
                <router-link to="/total" class="item-name">Tổng quan</router-link>

            </div>

            <div class="side-bar-item">
                <div class="icon-money side-bar-icon"></div>
                <router-link to="/employee" class="item-name">Nhân viên</router-link>
            </div>

            <div class="side-bar-item">
                <div class="icon-taxt side-bar-icon"></div>
                <router-link to="/department" class="item-name">Phòng ban</router-link>
            </div>

            <div class="side-bar-item">
                <div class="icon-bills side-bar-icon"></div>
                <router-link to="/position" class="item-name">Chức vụ</router-link>
            </div>

            <div class="side-bar-item">
                <div class="icon-analysis side-bar-icon"></div>
                <router-link to="/statistic" class="item-name">Phân tích tài chính</router-link>
            </div>
        </div>
        
    </div>
  </template>
  
  <script>
  export default {
    methods: {
        /**
         * Lấy vị trí con chuột
         * author: LTQN(2/10/2022)
         * @param {*} event 
         */
        getPosition(event){
            this.posX = 150;
            this.posY = event.pageY;
        },

        /**
         * Hiển thị tooltip
         * author: LTQN(2/10/2022)
         * @param {string} content : nội dung
         */
        showTooltip(content){
            this.$emit('showTooltip', {contentTooltip: content, positionX: this.posX, positionY: this.posY});
           
        },

        /**
         * Ẩn tooltip
         * author: LTQN(2/10/2022)
         */
        hideTooltip(){
            this.$emit('hideTooltip');
        }
    },
    data(){
        return{
            posX: null,
            posY: null
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
   @import url(../../css/layout/sideBar.css);
  </style>
  