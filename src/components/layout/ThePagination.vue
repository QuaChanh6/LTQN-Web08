<template>
    <div class="pagination">
        <div class="pagination__left">Tổng số: <span>{{totalRecord}}</span> bản ghi</div>
        <div class="pagination__right">
            <div class="rowOfPage">
                <TheDropListPage :contentPage='contentPage' @numberRecordOfPage='numberRecordOfPage'/>
                
            </div>
            <div class="paging">
                <div class="prev" @click="prev">Trước</div>
                <div class="numberList">
                    <div class="first number" v-show="lastPage" @click="chooseNumber(1)" :class="{'active': choose === 1}">1</div>
                    <div class="dot" v-show="viewLast">...</div>
                    <div class="second number" @click="chooseNumber(second)" :class="{'active': choose === second}" v-show="viewFirst">{{second}}</div>
                    <div class="third number" @click="chooseNumber(third)" :class="{'active': choose === third}" v-show="viewMiddle">{{third}}</div>
                    <div  class="fourth number" @click="chooseNumber(fourth)" :class="{'active': choose === fourth}" v-show="viewLast">{{fourth}}</div>
                    <div class="dot" v-show="viewFirst">...</div>
                    <div class="last number" v-show="lastPage" @click="chooseNumber(totalPage)" :class="{'active': choose === totalPage}">{{totalPage}}</div>
                </div>
                <div class="next" @click="next">Sau</div>
            </div>
        </div>
    </div>
  </template>

<script>
import TheDropListPage from "./TheDropListPage.vue";
import Resource from "@/common/Resource";
  export default {
    props: {
        totalPage : Number, //tổng trang
        totalRecord : Number, //tổng bản ghi
    },
    components: { TheDropListPage },
    methods: {
        /**
         * Chọn trang
         * author: LTQN(20/9/2022)
         * @param {int} numberPage // số được chọn
         */
        chooseNumber(numberPage){
            this.choose = numberPage; 
            this.$emit('changePage', this.choose);    
        },
        /**
         * Thay đổi giao diện khi ở giữa trang
         * author: LTQN(20/9/2022)
         * @param {int} num 
         */
        changeViewNumberMiddle(num){
            this.second = num;
            this.third = num + 1;
            this.fourth = num + 2;
        },

        /**Thay đổi giao diện khi ở đầu trang
         * author: LTQN(20/9/2022)
         */
        changeViewNumberFirst(){
            this.second = 2;
            this.third = 3;
        },

        /**Thay đổi giao diện khi ở cuối trang
         * author: LTQN(20/9/2022)
         */
        changeViewNumberLast(){
            this.fourth = this.totalPage - 1;
            this.third = this.totalPage - 2;
        },

        /**
         * Sự kiện ấn nút trước
         * author: LTQN(20/9/2022)
         */
        prev(){
            if(this.choose > 1){
                this.chooseNumber(this.choose - 1);
            }
        },

        /**
         * Sự kiện ấn nút Sau
         * author: LTQN(20/9/2022)
         */
        next(){
            if(this.choose < this.totalPage){
                this.chooseNumber(this.choose + 1);
            }
        },
        /**
         * Gửi số bản ghi trong 1 trang khi chọn
         * author: LTQN(20/9/2022)
         * @param {int} num ; Số bản ghi trong 1 trang
         */
        numberRecordOfPage(num){
            this.$emit('recordOfPage', num);
        }
    },
    data(){
        return{
            viewFirst: true, //giao diện khi ở các trang đầu
            lastPage: true, //giao diện ẩn hiện cho trang đầu và cuối
            viewLast: false, // giao diện khi ở các trang cuối
            viewMiddle: true, //ẩn hiện cho vị trí trang ở chính giữa
            pageChoose: 1, //trang hiện tại đang chọn
            second: 2,
            third: 3,
            fourth: null,
            choose: 1, //trang được chọn
            contentPage: Resource.textNumPages, //Nội dung lựa chọn số bản ghi 1 trang
    
        }
    },
    watch: {
        choose(newChoose){
            if(this.totalPage > 3){
                this.lastPage = true;
                if(newChoose <= 2){ // hiện giao diện khi ở trang đầu
                    this.viewLast = false;
                    this.viewFirst = true;
                    this.changeViewNumberFirst();
                }
                if((this.totalPage - 3) >= newChoose && newChoose > 2){
                    this.viewFirst = true; // hiện giao diện khi ở các trang giữa
                    this.viewLast = true;
                    this.changeViewNumberMiddle(newChoose);
                }
                if(newChoose > this.totalPage - 3){ // hiện giao diện khi ở các trang cuối
                    this.viewLast = true;
                    this.viewFirst = false;
                    this.changeViewNumberLast();

                }
            }
        },

        totalPage(newPage){ //hiển thị lại giao diện khi số trang thay đổi
            if(newPage > 3){
                this.viewFirst = true;
                this.viewLast = false;
                this.lastPage = true;
                this.viewMiddle = true;
                this.second = 2;
                this.third = 3;
            }else{
                this.viewFirst = false;
                this.viewLast = false;
                if(newPage == 1){ //chỉ có 1 trang
                    this.lastPage = false;
                    this.viewMiddle = true;
                    this.third = 1;
                }
                if(newPage == 2){ // chỉ có 2 trang
                    this.viewMiddle = false;
                    this.lastPage = true;
                }
                if(newPage == 3){ // chỉ có 3 trang

                    this.viewMiddle = true;
                    this.lastPage = true;
                    this.third = 2;
                }
            }
        }
    }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../css/layout/pagination.css);
  </style>
  