<template>
    <div class="dropList">
        <div class="text">Sửa</div>
        <div class="icon-drop" @click="isShowDropList =! isShowDropList" :class="{'icon-drop-click': isShowDropList}"></div>
        <div class="list-data" v-show="isShowDropList">
            <div class="data"  @click="indexChoose=index.Duplicate" :class="{'drop-data-choose': indexChoose==index.Duplicate}">Nhân bản</div>
            <div class="data delete" @click="sendMessageDelete(this.employeeID)" :class="{'drop-data-choose': indexChoose==index.Delete}">Xóa</div>
            <div class="data">Ngừng sử dụng</div>
        </div>
        
    </div>
    <!-- <MPopup :id='ID' v-if='isShow'/> -->
 
</template>
  
<script>


export default {
    props: {
        employeeID: String
    },
    methods: {
        // ấn nút xóa
        sendMessageDelete(id) {
            this.indexChoose=this.index.Delete;
            this.isShowDropList=false;
            this.$emit('showPopUp', id);
        }
    },
    data() {
        return {
            isShowDropList: false,
            indexChoose: null,
            index: {
                Duplicate: 1,
                Delete: 2,
                Stop: 3
            },
            isShow: false
        };
    },
    components: {}
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../css/base/dropList.css);
</style>
  