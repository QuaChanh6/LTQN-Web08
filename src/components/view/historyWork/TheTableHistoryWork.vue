<template>
    <div class="history-work">
        <div class="container"></div>
        <div class="container-work">
        <h2>Lịch sử công việc</h2>
        <div class="icon-x" @click="closeForm"></div>
        <div class="container-table-work">
        <table  @mousemove="getPosition" class="table-emp table-work">
            <thead>
                <tr>
                    <th class="positionW">CHỨC VỤ</th>
                    <th class="departmentW">PHÒNG BAN</th>
                    <th class="start">NGÀY BẮT ĐẦU</th>
                    <th class="end">NGÀY KẾT THÚC</th>
                </tr>
            </thead>           
            <tbody ref="bodyTable">
                <tr v-for="(work, index) in works" :key="index">
                    <td class="positionW">{{work.positionW}}</td>
                    <td class="departmentW">{{work.departmentW}}</td>
                    <td class="start">{{format(work.dateStart)}}</td>
                    <td class="end">{{format(work.dateEnd)}}</td>
                </tr>  
                            
            </tbody>
        </table>
    </div>
    </div>
    </div>
  
   

</template>

<script>
import format from '@/common/formatData';
export default {
    props:{
        employeeCode: String
    },
    created() {
        var requestOptions = {
        method: 'GET',
        redirect: 'follow'
        };

        fetch("https://localhost:7271/api/v1/HistoryWork/AllWork/"+ this.employeeCode, requestOptions)
        .then(response => response.json())
        .then(result => {
            this.works = result;
            console.log(result);
        })
        .catch(error => console.log('error', error));
        console.log(this.works);
    },
    methods:{
          /**
         * Hàm format date ngày/tháng/năm
         * author: (14/9/2022)
         * @param {String} date 
         */
         format(date){
            return format.formatDate(date);
        },
        closeForm(){
            this.$emit('closeForm');
        }
    },
    data(){
        return{
            works: [],
            isShow: false
        }
    }
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url("../../../css/historyWork/tableWork.css");
</style>
  