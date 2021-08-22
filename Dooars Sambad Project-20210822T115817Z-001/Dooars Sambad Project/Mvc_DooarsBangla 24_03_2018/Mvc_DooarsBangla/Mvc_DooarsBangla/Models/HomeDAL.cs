﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mvc_DooarsBangla.Models
{
    public class HomeDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ToString());
        public List<MarqueeModel> getAllMarqueeNewsToDisplay()
        {
            try
            { 
            SqlCommand com_getAllMarquee = new SqlCommand("select MarqueeNewsID,MarqueeDescription from MarqueeNews where MarqueeStatus='Visible' order by MarqueeNewsID desc", con);
            con.Open();
            SqlDataReader dr = com_getAllMarquee.ExecuteReader();
            List<MarqueeModel> lst_marquee = new List<MarqueeModel>();

            while (dr.Read())
            {
                MarqueeModel model = new MarqueeModel();
                model.MarqueeNewsID = dr.GetInt32(0);
                model.MarqueeDescription = dr.GetString(1);
               
                lst_marquee.Add(model);
            }
            //con.Close();
            return lst_marquee;
            }
            finally
            {
                con.Close();
            }
        }





        ////////////////login////////////////////////
        public CheckPasswordStatusEnum login(LoginModel model)
        {
            try
            {
                if (Membership.ValidateUser(model.LogInId.ToString(), model.Password))
                {
                    if (checkAdminStatus(model))
                    {
                        //con.Close();
                        return CheckPasswordStatusEnum.Updated;
                    }
                    else
                    {
                        // con.Close();
                        return CheckPasswordStatusEnum.NewUser;
                    }
                }
                else
                {
                    // con.Close();
                    return CheckPasswordStatusEnum.WrongPassword;
                }
            }
            finally
            {
                con.Close();
            }

        }

        public bool checkAdminStatus(LoginModel model)
        {
            try
            {
                SqlCommand com_check = new SqlCommand("select PasswordStatus from Admins where adminid=@aid", con);
                com_check.Parameters.AddWithValue("@aid", model.LogInId);
                con.Open();
                string passStatus = com_check.ExecuteScalar().ToString();
                if (passStatus == "AutoGenerated")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            finally
            {
                con.Close();
            }
        }

        ///////////////Atanu 12//////////////
        public List<AddScrollerImageModel> GetScrollerImage()
        {
            try
            { 
            SqlCommand com_GetScrlImg = new SqlCommand("select * from ScrollerImage order by Position", con);
            con.Open();
            SqlDataReader dr = com_GetScrlImg.ExecuteReader();
            List<AddScrollerImageModel> obj = new List<AddScrollerImageModel>();
            while (dr.Read())
            {
                AddScrollerImageModel model = new AddScrollerImageModel();
                model.ScrlImageID = dr.GetInt32(0);
                model.ImageHeader = dr.GetString(1);
                model.ImageDesc = dr.GetString(2);
                model.ImageAddress = dr.GetString(3);
                if (model.ScrlImageID != 10000)
                obj.Add(model);
            }
            //con.Close();
            return obj;
            }
            finally
            {
                con.Close();
            }
        }



        ///////////////////////// সর্বশেষ সংযোজন/////////////////////////
        public List<AllNewsTableModel> LatsetNews()
        {
            try
            { 
            SqlCommand com_ltnews = new SqlCommand("select NewsID, NewsTitle from AllNewstable where IsLatest='Yes' and PriorityOfNews= 'Yes' order by NewsDate desc ", con);
            con.Open();
            SqlDataReader dr= com_ltnews.ExecuteReader();
            List<AllNewsTableModel> lst_ltnewsTitle = new List<AllNewsTableModel>();

            while(dr.Read())
            {
                AllNewsTableModel model = new AllNewsTableModel();
                model.NewsID = dr.GetString(0);
                model.NewsTitle = dr.GetString(1);
                lst_ltnewsTitle.Add(model);
            }
            con.Close();
            con.Open();
            foreach(AllNewsTableModel item in lst_ltnewsTitle)
            {
                SqlCommand com_ltnewsPhoto = new SqlCommand("select ImageAddress from NewsPhoto where NewsID=@id and Section=1", con);
                com_ltnewsPhoto.Parameters.AddWithValue("@id", item.NewsID);
              
                item.Section1ImageString = com_ltnewsPhoto.ExecuteScalar().ToString();
            }
            con.Close();
            
            return lst_ltnewsTitle;
            }
            finally
            {
                con.Close();
            }
        }




        ///////////////////////// আকর্ষনীয় খবর/////////////////////////
        public List<AllNewsTableModel> PopulartNews()
        {
            try
            { 
            SqlCommand com_popnews = new SqlCommand("select top 4 NewsID, NewsTitle from AllNewstable where IsPopular='Yes' and PriorityOfNews= 'Yes' order by NewsDate desc ", con);
            con.Open();
            SqlDataReader dr = com_popnews.ExecuteReader();
            List<AllNewsTableModel> lst_popnewsTitle = new List<AllNewsTableModel>();

            while (dr.Read())
            {
                AllNewsTableModel model = new AllNewsTableModel();
                model.NewsID = dr.GetString(0);
                model.NewsTitle = dr.GetString(1);
                lst_popnewsTitle.Add(model);
            }
            con.Close();
            con.Open();
            foreach (AllNewsTableModel item in lst_popnewsTitle)
            {
                SqlCommand com_popnewsPhoto = new SqlCommand("select ImageAddress from NewsPhoto where NewsID=@id and Section=1", con);
                com_popnewsPhoto.Parameters.AddWithValue("@id", item.NewsID);

                item.Section1ImageString = com_popnewsPhoto.ExecuteScalar().ToString();
            }
            con.Close();
            return lst_popnewsTitle;
            }
            finally
            {
                con.Close();
            }
        }

        //////subha 13/12
        public List<AllNewsTableModel> getBlockAllNews(string block)
        {
            try
            { 
            SqlCommand com_getBlockAllNews = new SqlCommand("Select NewsID,NewsTitle,Section1Description,NewsDate From AllNewstable where Category=@cat order by NewsDate desc", con);
            com_getBlockAllNews.Parameters.AddWithValue("@cat", block);
            con.Open();
            SqlDataReader dr = com_getBlockAllNews.ExecuteReader();
            List<AllNewsTableModel> list_allnews = new List<AllNewsTableModel>();
            while (dr.Read())
            {
                AllNewsTableModel model = new AllNewsTableModel();
                model.NewsID = dr.GetString(0);
                model.NewsTitle = dr.GetString(1);
                if (model.NewsTitle.Length > 35)
                {
                    model.NewsTitle = model.NewsTitle.Substring(0, 35) + "....";
                }
                model.Section1Description = dr.GetString(2);
                if (model.Section1Description.Length > 90)
                {
                    model.Section1Description = model.Section1Description.Substring(0, 90) + "....";
                }
                model.NewsDate = dr.GetDateTime(3).ToShortDateString();
                list_allnews.Add(model);
            }
            con.Close();

            foreach (AllNewsTableModel item in list_allnews)
            {
                SqlCommand com_getImg = new SqlCommand("Select ImageAddress from NewsPhoto where newsid=@id and section=1", con);
                com_getImg.Parameters.AddWithValue("@id", item.NewsID);
                con.Open();
                item.Section1ImageString = com_getImg.ExecuteScalar().ToString();
                con.Close();
            }
            return list_allnews;
            }
            finally
            {
                con.Close();
            }
        }

        public AllNewsTableModel getBriefNews(string id)
        {
            try
            {
            SqlCommand com_getBriefNews = new SqlCommand("select * from AllNewstable where newsid=@id", con);
            com_getBriefNews.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = com_getBriefNews.ExecuteReader();
            AllNewsTableModel model = new AllNewsTableModel();
            while (dr.Read())
            {
                model.NewsID = dr.GetString(0);
                model.NewsTitle = dr.GetString(1);
                model.category = dr.GetString(2);
                model.Section1Description = dr.GetString(3);
                model.Section2Description = dr.GetString(4);
                model.Section3Description = dr.GetString(5);
                model.NewsDate = dr.GetDateTime(10).ToShortDateString();
            }
            con.Close();

            SqlCommand com_getiFrame = new SqlCommand("select Section,IFrameAddress from NewsVideo where NewsID=@nid", con);
            com_getiFrame.Parameters.AddWithValue("@nid", id);
            con.Open();
            SqlDataReader drx = com_getiFrame.ExecuteReader();
            int tempVal=0;
            NewsVideoModel model1 = new NewsVideoModel();
            if (drx.Read())
            {
                tempVal = 1;
                model1.Section = drx.GetInt32(0);
                model1.IFrameAddress = drx.GetString(1);
            }
            con.Close();
            if (tempVal == 1)
            {
                model.IFrameAddress = model1.IFrameAddress;
                model.Section = model1.Section;
            }

            if(model1.Section!=1)
            {
                SqlCommand com_getImgAddress1 = new SqlCommand("select ImageAddress from NewsPhoto where newsid=@id and section=1", con);
                com_getImgAddress1.Parameters.AddWithValue("@id", model.NewsID);
                con.Open();
                model.Section1ImageString = com_getImgAddress1.ExecuteScalar().ToString();
                con.Close();
            }
           
            if(model1.Section!=2)
            {
                SqlCommand com_getImgAddress2 = new SqlCommand("select ImageAddress from NewsPhoto where newsid=@id and section=2", con);
                com_getImgAddress2.Parameters.AddWithValue("@id", model.NewsID);
                con.Open();
                Object temp = com_getImgAddress2.ExecuteScalar();
                if (temp != null)
                {
                    model.Section2ImageString = temp.ToString();
                }
                con.Close();
            }


            if (model1.Section != 3)
            {
                SqlCommand com_getImgAddress3 = new SqlCommand("select ImageAddress from NewsPhoto where newsid=@id and section=3", con);
                com_getImgAddress3.Parameters.AddWithValue("@id", model.NewsID);
                con.Open();
                Object temp1 = com_getImgAddress3.ExecuteScalar();
                if (temp1 != null)
                {
                    model.Section3ImageString = temp1.ToString();
                }
                con.Close();
            }

            return model;
            }
            finally
            {
                con.Close();
            }
        }


        public string getNewsId(int mID)
        {
            try
            { 
            SqlCommand com_getNewsID = new SqlCommand("select NewsID from AllNewstable where MarqueeNewsID=@id", con);
            com_getNewsID.Parameters.AddWithValue("@id", mID);
            con.Open();
            Object temp=com_getNewsID.ExecuteScalar();
            if(temp!=null)
            {
                string NiD = temp.ToString();
                return NiD;
            }
            return null;
            }
            finally
            {
                con.Close();
            }
        }



        public string getNewsIdForScroller(int sID)
        {
            try
            { 
            SqlCommand com_getNewsID = new SqlCommand("select NewsID from AllNewstable where ScrImageID=@id", con);
            com_getNewsID.Parameters.AddWithValue("@id", sID);
            con.Open();
            Object temp = com_getNewsID.ExecuteScalar();
            if (temp != null)
            {
                string NiD = temp.ToString();
                return NiD;
            }
            return null;
            }
            finally
            {
                con.Close();
            }
        }





        ///////////////souvik 15/12 //////////////
        public List<PhotoGalleryModel> GetGalleryImage()
        {
            try
            { 
            SqlCommand com_GetScrlImg = new SqlCommand("select * from Gallery order by Position", con);
            con.Open();
            SqlDataReader dr = com_GetScrlImg.ExecuteReader();
            List<PhotoGalleryModel> obj = new List<PhotoGalleryModel>();
            while (dr.Read())
            {
                PhotoGalleryModel model = new PhotoGalleryModel();
                model.imageID = dr.GetInt32(0);
                model.ImageTitle = dr.GetString(1);
                model.ImageAddress = dr.GetString(2);
                //if (model.imageID != 10000)
                obj.Add(model);
            }
           // con.Close();
            return obj;
            }
            finally
            {
                con.Close();
            }
        }

        //////////////////FeedBack System////////////////
        public bool AddFeedBack(AddFeedBackModel model)
        {
            try
            { 
            SqlCommand com_AddFeedback = new SqlCommand("insert FeedBack values(@name,@no,@message,'Invisible')", con);
            com_AddFeedback.Parameters.AddWithValue("@name", model.Name);
            com_AddFeedback.Parameters.AddWithValue("@no", model.contactNumber);
            com_AddFeedback.Parameters.AddWithValue("@message", model.Message);
            con.Open();
            int i = com_AddFeedback.ExecuteNonQuery();
            if (i == 1)
            {
                //con.Close();
                return true;
            }
           // con.Close();
            return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool CHangeFeedbackStatus(int Fid, string status)
        {
            try
            { 
            SqlCommand com_ChangeSatus = new SqlCommand("Update FeedBack set FeedbackStatus=@status where FeedBackID=@id", con);
            com_ChangeSatus.Parameters.AddWithValue("@status", status);
            com_ChangeSatus.Parameters.AddWithValue("@id", Fid);
            con.Open();
            int i = com_ChangeSatus.ExecuteNonQuery();
            if (i == 1)
            {
                //con.Close();
                return true;
            }
            //con.Close();
            return false;
            }
            finally
            {
                con.Close();
            }

        }


        //////////////////////////////showing all visible feedback for people////////////////////
        public List<AddFeedBackModel> showAllVisibleFeedback()
        {
            try
            {
                SqlCommand com_showFeedback = new SqlCommand("select * from feedback where feedbackStatus='Visible' order by feedbackID desc", con);
                con.Open();
                SqlDataReader dr = com_showFeedback.ExecuteReader();
                List<AddFeedBackModel> list_feed = new List<AddFeedBackModel>();
                while (dr.Read())
                {
                    AddFeedBackModel model = new AddFeedBackModel();
                    model.FeedbackID = dr.GetInt32(0);
                    model.Name = dr.GetString(1);
                    model.contactNumber = dr.GetString(2);
                    model.Message = dr.GetString(3);
                    model.feedbackStatus = dr.GetString(4);
                    list_feed.Add(model);
                }

                //con.Close();
                return list_feed;
            }
            finally
            {
                con.Close();
            }
        }

        public List<AllNewsTableModel> RelatedNews(string newsid,string category)
        {
            try
            { 
            SqlCommand com_popnews = new SqlCommand("select top 7 NewsID, NewsTitle from AllNewstable where Category=@cat and NewsID<> @id order by NewsDate desc ", con);
            com_popnews.Parameters.AddWithValue("@id",newsid);
            com_popnews.Parameters.AddWithValue("@cat", category);
            con.Open();
            SqlDataReader dr = com_popnews.ExecuteReader();
            List<AllNewsTableModel> lst_popnewsTitle = new List<AllNewsTableModel>();

            while (dr.Read())
            {
                AllNewsTableModel model = new AllNewsTableModel();
                model.NewsID = dr.GetString(0);
                model.NewsTitle = dr.GetString(1);
                lst_popnewsTitle.Add(model);
            }
            con.Close();
            con.Open();
            foreach (AllNewsTableModel item in lst_popnewsTitle)
            {
                SqlCommand com_popnewsPhoto = new SqlCommand("select ImageAddress from NewsPhoto where NewsID=@id and Section=1", con);
                com_popnewsPhoto.Parameters.AddWithValue("@id", item.NewsID);

                item.Section1ImageString = com_popnewsPhoto.ExecuteScalar().ToString();
            }
            con.Close();
            return lst_popnewsTitle;
            }
            finally
            {
                con.Close();
            }
        }

        public NewsPaperPageModel getNewsPaperPage(int pNo)
        {
            try
            {
                SqlCommand com_GetScrlImg = new SqlCommand("select * from NewsPaperPage where PageNumber=@no", con);
                com_GetScrlImg.Parameters.AddWithValue("@no", pNo);
                con.Open();
                SqlDataReader dr = com_GetScrlImg.ExecuteReader();
                NewsPaperPageModel model = new NewsPaperPageModel();
                if (dr.Read())
                {

                    model.PaperImageID = dr.GetInt32(0);
                    model.ImageAddress = dr.GetString(1);
                    model.PageNumber = dr.GetInt32(2);

                }
                // con.Close();
                return model;
            }
            finally
            {
                con.Close();
            }
        }

        public int getNoOfPages()
        {
            try
            {
                SqlCommand com_get = new SqlCommand("select count(*) from NewsPaperPage", con);
                con.Open();
                int c = Convert.ToInt32(com_get.ExecuteScalar());
                //con.Close();
                return c;
            }
            finally
            {
                con.Close();
            }
        }
        
    }
}