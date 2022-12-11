<template>
    <div class="container-table table-position">
        <table class="tablePos">
            <thead>
                <tr>
                    <th class="positionCode">MÃ CHỨC VỤ</th>
                    <th class="positionName">TÊN CHỨC VỤ</th>
                    <th class="numberOfEmployee">SỐ LƯỢNG NHÂN VIÊN</th>
                    <th class="manager">QUẢN LÝ</th>
                </tr>
            </thead>           
            <tbody>
                <tr @dblclick="showForm" v-for="(position, index) in positions" :key="index">
                    <td class="positionCode">{{position.positionCode}}</td>
                    <td class="positionName">{{position.positionName}}</td>
                    <td class="numberOfEmployee">{{getCountOfPosition(position.positionID)}}</td>
                    <td class="manager">{{position.manager}}</td>
                </tr>  
                            
            </tbody>
        </table>
        <!-- <div class="loadingTable" v-show="isLoadTable"><div v-for="i in 10" :key="i" class="container-skeleton">
            <div  class="skeleton" v-for="i in 5" :key="i"></div>
        </div> -->
    </div>

  </template>
  
  <script>
  import format from '@/common/formatData';
  export default {
    created(){

        let url = process.env.VUE_APP_URL+'Positions';

        if(!format.checkEmptyData(localStorage.getItem('position'))){
            this.positions=JSON.parse(localStorage.getItem('position'));
            }
            else{//chưa có dữ liệu trong localstorage
                fetch(url)
                .then(res => res.json())
                .then(res => {
                    this.positions = res;              
                    localStorage.setItem('position', JSON.stringify(res));
                }).catch(error => {
                    console.log(error);
            }) 
        }
        url= process.env.VUE_APP_URL+'Positions/countEmployee';
        fetch(url)
            .then(res => res.json())
            .then(res => {
                this.counts = res;              
            }).catch(error => {
                console.log(error);
        }) 

        },
    methods: {
        showForm(){
            this.$emit('showForm');
        },

        getCountOfPosition(id){

        for(let count in this.counts){
            let pos = this.counts[count];
            if(pos.positionID == id){

                return pos.count;
            }
        }

        return 0;
        }
    },
    data(){
        return{
            positions: [],
            counts: [],
        }
    }
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  .table-position{
    height: 550px;
  }

  table{
    width: 100%;
  }
  </style>
  