<template>
    <div class="pagination">
        <div class="pagination__left">Tổng số: <span>1035</span> bản ghi</div>
        <div class="pagination__right">
            <div class="rowOfPage">
                <TheDropListPage/>
                
            </div>
            <div class="paging">
                <div class="prev">Trước</div>
                <div class="numberList">
                    <div class="1 number active">1</div>
                    <div class="2 number">2</div>
                    <div class="3 number">3</div>
                    <div class="dot">...</div>
                    <div class="numberLast number">52</div>
                </div>
                <div class="next">Sau</div>
            </div>
        </div>
    </div>
  </template>

  <script>
import TheDropListPage from "./TheDropListPage.vue";

  export default {
    components: { TheDropListPage }
}
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  @import url(../../css/layout/pagination.css);
  </style>
  