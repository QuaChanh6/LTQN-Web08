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
                    <td class="role">{{user.role }}</td>
                </tr>  
                            
            </tbody>
        </table>
    </div>
  
  </template>
  
  <script>

import format from '@/common/formatData';


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

  export default {
    props:{
        search: String
    },
    created(){
        let url =  process.env.VUE_APP_URL+"Users";
        if(!format.checkEmptyData(this.search)){
            url =  url + "?keyword=" + this.search;
        }
        getUserAsync(url).then(data =>{
                this.users = data;
           
            })
    },
    data(){
        return{
            users: []
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../../css/layout/tableuser.css);
  </style>
  