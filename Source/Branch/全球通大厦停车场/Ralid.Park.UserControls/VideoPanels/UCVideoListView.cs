﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls.VideoPanels
{
    public partial class UCVideoListView : UserControl
    {
        public UCVideoListView()
        {
            InitializeComponent();

            VideoPanel video = VideoPanelFactory.CreatePanel();
            video.AllowDrop = true;
            video.Name = "actiVideoPanel0";
            video.ShowTitle = true;
            video.StretchToFit = true;
            video.VideoSource = null;
            video.Visible = true;
            video.Dock = DockStyle.Fill;
            this.btnLeft.Visible = false;
            this.btnRight.Visible = false;
            this.videoPanel.Controls.Add(video);
            _Videoes.Add(video);
            Clear();
        }

        #region 私有变量
        List<VideoPanel> _Videoes = new List<VideoPanel>();
        List<VideoPanel> _ActiveVideoes = new List<VideoPanel>();
        private int _CurImageIndex = 0;
        #endregion

        #region 私有方法
        private void btnLeft_Click(object sender, EventArgs e)
        {
            _CurImageIndex--;
            ShowVideo(_CurImageIndex);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _CurImageIndex++;
            ShowVideo(_CurImageIndex);
        }

        private void ShowVideo(int index)
        {
            this.btnLeft.Enabled = (index > 0);
            this.btnRight.Enabled = (index >= 0 && index < _ActiveVideoes.Count - 1);
            foreach (VideoPanel vp in _Videoes)
            {
                vp.Visible = false;
            }
            if (index >= 0 && index < _ActiveVideoes.Count)
            {
                _ActiveVideoes[index].Dock = DockStyle.Fill;
                _ActiveVideoes[index].Visible = true;
                if (_ActiveVideoes[index].Status != VideoStatus.Playing)
                {
                    _ActiveVideoes[index].Play(true);
                }
            }
            else if (_Videoes.Count > 0)
            {
                _Videoes[0].Visible = true;
            }
        }
        #endregion

        #region 公共方法和属性
        /// <summary>
        /// 在视频列表中显示一系列图片
        /// </summary>
        /// <param name="imgs"></param>
        public void ShowVideoes(List<VideoSourceInfo> videoes)
        {
            this.btnLeft.Visible = !(videoes == null || videoes.Count <= 1);
            this.btnRight.Visible = !(videoes == null || videoes.Count <= 1);

            _ActiveVideoes.Clear();
            if (videoes != null)
            {
                for (int i = _Videoes.Count; i < videoes.Count; i++)
                {
                    VideoPanel video = VideoPanelFactory.CreatePanel();
                    video.AllowDrop = true;
                    video.Name = "actiVideoPanel" + i.ToString();
                    video.ShowTitle = true;
                    video.StretchToFit = true;
                    video.VideoSource = null;
                    this.videoPanel.Controls.Add(video);
                    _Videoes.Add(video);
                }
                foreach (VideoPanel vp in _Videoes)
                {
                    if (!videoes.Exists(v => v == vp.VideoSource))
                    {
                        if (vp.Status == VideoStatus.Playing)
                        {
                            vp.Close();
                        }
                        vp.VideoSource = null;
                    }
                }

                foreach (VideoSourceInfo vs in videoes)
                {
                    VideoPanel video = _Videoes.FirstOrDefault(vp => vp.VideoSource == vs);
                    if (video == null)
                    {
                        VideoPanel p = _Videoes.FirstOrDefault(vp => vp.VideoSource == null);
                        if (p != null)
                        {
                            p.VideoSource = vs;
                            _ActiveVideoes.Add(p);
                        }
                    }
                    else
                    {
                        _ActiveVideoes.Add(video);
                    }
                }
            }

            if (_ActiveVideoes.Count > 0)
            {
                _CurImageIndex = 0;
                ShowVideo(_CurImageIndex);
            }
            else
            {
                _CurImageIndex = -1;
                ShowVideo(_CurImageIndex);
            }
        }

        /// <summary>
        /// 清空所有视频
        /// </summary>
        public void Clear()
        {
            foreach (VideoPanel  vp in _ActiveVideoes)
            {
                if (vp.Status == VideoStatus.Playing)
                {
                    vp.Close();
                }
                vp.VideoSource = null;
            }
            _CurImageIndex = -1;
            ShowVideo(_CurImageIndex);
        }

        #endregion
    }
}
