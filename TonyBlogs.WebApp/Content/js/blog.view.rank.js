Vue.component('blogview', {
    props: ['userid'],
    template: '\
        <div>\
            <h3 style="margin-bottom: 20px;">最近阅读排行</h3>\
            <div class="recent-news margin-bottom-10">\
                <div v-for="viewblog in ViewRankList" class="row margin-bottom-10">\
                    <div class="col-md-12">\
                        <a v-bind:href="DetailUrl(viewblog.ID)" style="color: #E84D1C" target="_blank">{{viewblog.Title}}</a>\
                    </div>\
                </div>\
            </div>\
        </div>\
     ',
    data: function () {
        return {
            ViewRankList: [],
            list_url: '/Home/AjaxGetViewRankList',
            detail_url: '/b/',
        };
    },
    methods: {
        GetViewRankList: function () {
            var that = this;

            $.ajax({
                url: that.list_url,
                data: { userid: that.userid },
                type: "GET",
                dataType: "json",
                success: function (retData) {
                    that.ViewRankList = retData;
                },
                error: function (retData) {
                    Metronic.simpleAlert("发生错误，请重试！");
                }
            })
        },
        DetailUrl: function (blogId) {
            return this.detail_url + blogId;
        },
    },
    mounted: function () {
        this.GetViewRankList();
    },
})