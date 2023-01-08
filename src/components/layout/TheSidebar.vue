<template>
    <div class="side-bar" @mousemove="getPosition" @mouseleave="hideTooltip">
        <div class="side-bar-title">
            <!-- <div class="select-option-icon"></div> -->
            <div class="logo"></div>
            <div class="title-bar"> QUẢN LÝ HỒ SƠ</div>
        </div>
        <div class="info-user">
            <div class="avt"></div>
            <div class="name-user">Xin chào, {{name}}</div>
            <div class="employeeCode-user">{{code}}</div>

        </div>
        <div class="side-bar-list">
            <div class="side-bar-item" v-if="role == 0" :class="{'selected-item': selected == 1}" @click="select(1)">
                <div class="icon-total side-bar-icon"></div>
                <router-link to="/total" class="item-name">Tổng quan</router-link>

            </div>
            
            <div class="side-bar-item" v-if="role == 1 || role == 2"  :class="{'selected-item': selected == 2}" @click="select(2)">
                <div class="icon-info side-bar-icon"></div>
                <router-link to="/employeeDetail" class="item-name">Thông tin cá nhân</router-link>
            </div>

            <div class="side-bar-item" v-if="role == 1 || role == 2" :class="{'selected-item': selected == 3}" @click="select(3)">
                <div class="icon-bills side-bar-icon"></div>
                <router-link to="/appForm" class="item-name">Quản lý đơn từ</router-link>
            </div>

            <div class="side-bar-item" v-if="role == 0 || role == 2"  :class="{'selected-item': selected == 4}" @click="select(4)">
                <div class="icon-employee side-bar-icon"></div>
                <router-link to="/employee" class="item-name">Nhân viên</router-link>
            </div>
            <div class="side-bar-item" v-if="role == 0" @click="select(5)" :class="{'selected-item': selected == 5}">
                <div class="icon-money side-bar-icon"></div>
                <router-link to="/user" class="item-name">Quản lý tài khoản</router-link>
            </div>

            <div class="side-bar-item" v-if="role == 0" @click="select(6)" :class="{'selected-item': selected == 6}">
                <div class="icon-taxt side-bar-icon"></div>
                <router-link to="/department" class="item-name">Phòng ban</router-link>
            </div>

            <div class="side-bar-item" v-if="role == 0" @click="select(7)" :class="{'selected-item': selected == 7}">
                <div class="icon-bills side-bar-icon"></div>
                <router-link to="/position" class="item-name">Chức vụ</router-link>
            </div>

            <div class="side-bar-item"  v-if="role == 0 || role == 2" @click="select(8)" :class="{'selected-item': selected == 8}">
                <div class="icon-analysis side-bar-icon"></div>
                <router-link to="/statistic" class="item-name">Lương nhân viên</router-link>
            </div>
        </div>
        
    </div>
  </template>
  
  <script>
  export default {
    props:{
        name: String,
      role: Number,
      code: String
    },
    created(){ 
        if(this.role == 0){
        this.selected = 1;
        }else{
            this.selected = 2;
        }
            


    },
    methods: {
        select(e){
            this.selected = e;
        },
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
            posY: null,
            selected : null
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
   @import url(../../css/layout/sideBar.css);

   .logo{
    background-image: url("../../../public/favicon.ico");
    height: 32px;
    width: 32px;
    background-size: cover;
    margin-top: 10px;
    margin-left: 16px;
   }
  </style>
  