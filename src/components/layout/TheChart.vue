<template>
    <div class="chart">
        <Bar
      :chart-options="chartOptions"
      :chart-data="chartData"
      :chart-id="chartId"
      :dataset-id-key="datasetIdKey"
      :plugins="plugins"
      :css-classes="cssClasses"
      :styles="styles"
      :width="width"
      :height="height"
    />
    </div>

</template>
  
  <script>
  import { Bar } from 'vue-chartjs'
  import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'


  async function getUserAsync(url) {
        try {
            let response = await fetch(url);
            let data = await response.json();
            return data;
        } catch (error) {   
            console.log(error);
            return [];
        }
        
    }

  ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)
  
  export default {
    name: 'BarChart',
    components: { Bar },
    props: {
      chartId: {
        type: String,
        default: 'bar-chart'
      },
      datasetIdKey: {
        type: String,
        default: 'label'
      },
      width: {
        type: Number,
        default: 100
      },
      height: {
        type: Number,
        default: 100
      },
      cssClasses: {
        default: '',
        type: String
      },
      styles: {
        type: Object,
        default: () => {}
      },
      plugins: {
        type: Object,
        default: () => {}
      },
      label: Array,
      type: String,
      color: String
    },
    created(){
        this.chartData.labels = this.label;
        console.log(this.label);
        let url = process.env.VUE_APP_URL+`Employees/count${this.type}`;
        getUserAsync(url).then(data =>{
            this.count = data;
            this.update();
        });    
        
       
    },
    methods:{

        update(){
          let typeLabel = `${this.type}Name`;
           for(let j=0; j< this.chartData.labels.length; j++){
                for(let i = 0; i<this.count.length; i++){
                    if(this.count[i][typeLabel] === this.chartData.labels[j])
                        this.chartData.datasets[0].data[j] = this.count[i].count;   
                }
           }
            
        },
     
    },
    data() {
      return {
        chartData: {
          labels: [],
          datasets: [ { label: "Số lượng", backgroundColor: this.color, data: [] } ]
        },
        chartOptions: {
          responsive: true
        },
        count:[]
      }
    },
  }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import url(../../css/layout/chart.css);
</style>
