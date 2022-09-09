<template>
    <div class="dropList">
        <div class="text">Sửa</div>
        <div class="icon-drop" @click="isShowDropList =! isShowDropList" :class="{'icon-drop-click': isShowDropList}"></div>
        <div class="list-data" v-show="isShowDropList">
            <div class="data"  @click="indexChoose=index.Duplicate" :class="{'drop-data-choose': indexChoose==index.Duplicate}">Nhân bản</div>
            <div class="data delete" @click="indexChoose=index.Delete" :class="{'drop-data-choose': indexChoose==index.Delete}">Xóa</div>
            <div class="data">Ngừng sử dụng</div>
        </div>
        
    </div>
</template>
  
<script>
export default {
    data(){
        return{
            isShowDropList: false,
            indexChoose: null,
            index:{
                Duplicate: 1,
                Delete: 2,
                Stop: 3
            }
        }
    }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  