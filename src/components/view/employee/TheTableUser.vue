<template>
       <div class="user-table">
        <table style="width:100%;" class="tb-user">
            <thead>
                <tr>
                    <th class="username af">TÊN TÀI KHOẢN</th>
                    <th class="code af">MÃ NHÂN VIÊN</th>
                    <th class="name af">TÊN NHÂN VIÊN</th>
                    <th class="role af">VAI TRÒ</th>
                </tr>
            </thead>           
            <tbody ref="bodyTable">
                <tr v-for="(user, index) in users" :key="index">
                    <td class="username">{{user.userName}}</td>
                    <td class="code">{{user.employeeCode}}</td>
                    <td class="name">{{user.nameOfUser}}</td>
                    <td class="role sticky-col" :ref="'col_'+index" style="width: 200px;">
                        <div class="tool">
                            <div class="textTool" style="width: 150px;" >{{handleType(user.role) }}</div>
                            <div class="icon-drop" 
                            :class="{'icon-drop-click': isShowDataDropList[index]}" 
                            @click="getPositionTop(index)"></div>

                        </div>
                    </td>
                    <MDropList  v-if="isShowDataDropList[index]" :style="{'top': `${topDropList}px`}" @closeDropList="closeDropList"
                    @tool = 'tool' :emp='user' :dataDropList = 'dataDropList' :index = 'index'/> 
                </tr>  
                            
            </tbody>
        </table>
    </div>
  
  </template>
  
<script>

import format from '@/common/formatData';
import MDropList from '../../base/MDropList.vue';
import resource from '../../../common/resource';

/**
 * Lấy thông tin nhân viên
 * author: LTQN(10/9/2022)
 * @param {string} url 
 */
 async function getUserAsync(url) {
      try {
          let response = await fetch(url);
          if(response.ok){
              let data = await response.json();
              return data;

          }else{//lỗi

              return [];
          }
      } catch (error) {   
          console.log(error);
          return [];
      }
      
  }

   /**
   * hàm xử lý kết quả trả về
   * author: LTQN(10/9/2022)
   * @param {object} res: kết quả trả về của API
   * @param {int} pointer: {this, mode}
   */
   async function handleResponse(res, pointer){
      try {
            // thực hiện thành công
          if(res.ok){
              pointer.$emit("reload");
          } //thực hiện thất bại
          else{ //lỗi khác: >=400 && <600
              
          }
  
      } catch (error) {
          console.log(error);
      }

  }

export default {
  props:{
      search: String
  },
  components: { MDropList},
  created(){
      let url =  process.env.VUE_APP_URL+"Users";
      if(!format.checkEmptyData(this.search)){
          url =  url + "?keyword=" + this.search;
      }
      getUserAsync(url).then(data =>{
              this.users = data;
         
      })
  },
  methods:{
 /**
       * author: LTQN (14/9/2022)
       * Hàm set top cho droplist
       * @param {int} index : vị trí trong mảng employee
       */
       getPositionTop(index){
          try {
              this.isShowDataDropList[index] = !this.isShowDataDropList[index];
              let td = this.$refs['col_'+index];
              this.topDropList = td[0].getBoundingClientRect().top + 35;
          } catch (error) {
              console.log(error);
          }

      },
      closeDropList(){
          this.isShowDataDropList = [];
      },
      handleType(e){
          switch(e){
              case '0':
                  return "Admin";
              case '1':
                  return "Nhân viên";
              case '2':
                  return "Nhân viên quản lý";
          }
      },
       /**
       * Hàm xóa/ nhân bản/ ngừng sử dụng
       * author: LTQN (15/9/2022)
       */
       async tool(e){
          try {
              let emp = e.emp;
              if(e.tool === resource.role.Manager){ 

                  emp.role = '2';
              } 
              if(e.tool === resource.role.Employee){ 
                  emp.role = '1';
              } 

              let url= `${this.Url}Users/Role`;
              let response = await fetch(url, {
                  method: 'PUT',
                  headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
                  body: JSON.stringify(emp),
              }) 

              handleResponse(response, this);

              this.isShowDataDropList = [];
          } catch (error) {
              console.log(error);
          }
          
      },
  },
  data(){
      return{
          users: [],
          isShowDataDropList: [],
          dataDropList: resource.datarole,
          topDropList: null,
          Url: process.env.VUE_APP_URL,
      }
  }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/tableuser.css);
  </style>
  