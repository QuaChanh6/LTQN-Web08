<template>
    <div class="attach-file">
        <div class="container"></div>
        <div class="container-file">
            <h2>File đính kèm <span class="import" @click="importFile()"></span></h2>
            <input   :key="keyUpLoad" ref="file" class="fileImport" style="visibility: hidden;" @change="handleFileUpload()"  type="file"/>
            <div class="icon-x" @click="closeForm"></div>
            <span class="line"></span>
            <ul for="(file, index) in files">
                <!-- <li><span :href="file.href">{{file.name}}</span></li> -->
                <li>Link</li>
                <li>Link</li>
                <li>Link</li>
                <li>Link</li>
            </ul>
        </div>
    </div>
</template>

<script>
async function handleResponse(res, pointer){
        try {
              // thực hiện thành công
            if(res.ok){
              console.log(pointer);
              pointer.reloadAll();
            } //thực hiện thất bại
            else{ //lỗi khác: >=400 && <600
                console.log(res.ok);
            }
    
        } catch (error) {
            console.log(error);
        }
      
    }
export default {
    props:{
        employeeCode:String
    },
    created(){

    },
    methods:{
        importFile(){
          this.$refs.file.click(); 
        },
        closeForm(){
            this.$emit("closeFile");
        },
        async handleFileUpload(){
            var data = new FormData();
                data.append('file',  this.$refs.file.files[0]);
                data.append('user', 'admin');

                const  response = await fetch(process.env.VUE_APP_URL+'Employees/File/' + this.employeeCode, {
                method: 'POST',
                body: data
                })
                handleResponse(response, this);
        },
    },
    data(){
        return{
            files: []
        }
    }
}
</script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url(../../../css/layout/attachFile.css);
</style>
  