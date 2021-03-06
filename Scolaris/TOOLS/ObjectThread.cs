﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Scolaris.TOOLS
{
    class ObjectThread
    {
        #region DataGridView
        DataGridView _dataGrid;
        public ObjectThread(DataGridView _data_)
        {
            this._dataGrid = _data_;
        }

        public delegate void delegateClearDataGridView(bool i);
        public void ClearDataGridView(bool i)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateClearDataGridView deleg = new delegateClearDataGridView(ClearDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { i });
                }
                else
                {
                    _dataGrid.Rows.Clear();
                }
            }
        }

        public delegate void delegateRemoveDataGridView(int i);
        public void RemoveDataGridView(int i)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateRemoveDataGridView deleg = new delegateRemoveDataGridView(RemoveDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { i });
                }
                else
                {
                    if (i > -1)
                        _dataGrid.Rows.RemoveAt(i);
                }
            }
        }

        public delegate void delegateInsertDataGridView(object[] data);
        public void WriteDataGridView(object[] data)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateInsertDataGridView deleg = new delegateInsertDataGridView(WriteDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { data });
                }
                else
                {
                    _dataGrid.Rows.Add(data);
                }
            }
        }

        public delegate void delegateUpdateDataGridView(int i, object[] data);
        public void WriteDataGridView(int i, object[] data)
        {
            if (_dataGrid != null)
            {
                if (_dataGrid.InvokeRequired)
                {
                    delegateUpdateDataGridView deleg = new delegateUpdateDataGridView(WriteDataGridView);
                    _dataGrid.Invoke(deleg, new object[] { i, data });
                }
                else
                {
                    _dataGrid.Rows.Insert(i, data);
                }
            }
        }
        #endregion

        #region ListBox
        ListBox _listView;
        public ObjectThread(ListBox _data_)
        {
            this._listView = _data_;
        }

        public delegate void delegateUpdateListBox(string text);
        public void WriteListBox(string text)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateUpdateListBox deleg = new delegateUpdateListBox(WriteListBox);
                    _listView.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _listView.Items.Add(text);
                }
            }
        }

        public delegate void delegateClearListBox(bool clear);
        public void ClearListBox(bool clear)
        {
            if (_listView != null)
            {
                if (_listView.InvokeRequired)
                {
                    delegateClearListBox deleg = new delegateClearListBox(ClearListBox);
                    _listView.Invoke(deleg, new object[] { clear });
                }
                else
                {
                    _listView.Items.Clear();
                }
            }
        }
        #endregion

        #region Form
        Form _form;
        public ObjectThread(Form _data_)
        {
            this._form = _data_;
        }
        public delegate void delegateUpdateTextForm(string text);
        public void WriteTextForm(string text)
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateUpdateTextForm deleg = new delegateUpdateTextForm(WriteTextForm);
                    _form.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _form.Text = text;
                }
            }
        }

        public delegate void delegateDisposeForm(bool dispose);
        public void DisposeForm(bool dispose)
        {
            if (_form != null)
            {
                if (_form.InvokeRequired)
                {
                    delegateDisposeForm deleg = new delegateDisposeForm(DisposeForm);
                    _form.Invoke(deleg, new object[] { dispose });
                }
                else
                {
                    _form.Close();
                    if (dispose)
                        _form.Dispose();
                }
            }
        }

        #endregion

        #region Button
        Button _btn;
        public ObjectThread(Button _btn_)
        {
            this._btn = _btn_;
        }

        public delegate void delegateEnableButton(bool enable);
        public void EnableButton(bool enable)
        {
            if (_btn != null)
            {
                if (_btn.InvokeRequired)
                {
                    delegateEnableButton deleg = new delegateEnableButton(EnableButton);
                    _btn.Invoke(deleg, new object[] { enable });
                }
                else
                {
                    _btn.Enabled = enable;
                }
            }
        }

        public delegate void delegateTextButton(string text);
        public void TextButton(string text)
        {
            if (_btn != null)
            {
                if (_btn.InvokeRequired)
                {
                    delegateTextButton deleg = new delegateTextButton(TextButton);
                    _btn.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _btn.Text = text;
                }
            }
        }

        #endregion

        #region ProgressBar
        ProgressBar _bar;
        public ObjectThread(ProgressBar _bar_)
        {
            this._bar = _bar_;
        }
        public delegate void delegateUpdateBar(int value);
        public void UpdateBar(int value)
        {
            _UpdateBar(value, "Veuillez Patientez");
        }

        public delegate void _delegateUpdateBar(int value, string msg);
        public void _UpdateBar(int value, string msg)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    _delegateUpdateBar deleg = new _delegateUpdateBar(_UpdateBar);
                    _bar.Invoke(deleg, new object[] { value, msg });
                }
                else
                {
                    int v = _bar.Value;
                    v += value;
                    string percent_ = msg + "... (";
                    if (v > _bar.Maximum)
                    {
                        _bar.Value = _bar.Maximum;
                        percent_ = "Opération Terminée... (";
                    }
                    else
                    {
                        _bar.Value = v;
                    }
                    int percent = (int)(((double)(_bar.Value - _bar.Minimum) / (double)(_bar.Maximum - _bar.Minimum)) * 100);
                    percent = percent > 0 ? (percent < 100 ? percent : 100) : 0;
                    percent_ += percent.ToString() + "%)";
                    using (Graphics gr = _bar.CreateGraphics())
                    {
                        float x_ = _bar.Width / 2 - (gr.MeasureString(percent_, SystemFonts.DefaultFont).Width / 2.0F);
                        float y_ = _bar.Height / 2 - (gr.MeasureString(percent_, SystemFonts.DefaultFont).Height / 2.0F);
                        PointF p_ = new PointF(x_, y_);
                        gr.DrawString(percent_, SystemFonts.DefaultFont, Brushes.Black, p_);
                        if (percent >= 100)
                        {
                            Thread.SpinWait(1);
                        }
                    }
                }
            }
        }

        public void UpdateSimpleBar(int value)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateUpdateBar deleg = new delegateUpdateBar(UpdateSimpleBar);
                    _bar.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _bar.Value += value;
                }
            }
        }

        public void SetValueBar(int value)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateUpdateBar deleg = new delegateUpdateBar(SetValueBar);
                    _bar.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _bar.Value = value;
                }
            }
        }

        public delegate void delegateUpdateMaxBar(int value);
        public void UpdateMaxBar(int value)
        {
            try
            {
                if (_bar != null)
                {
                    if (_bar.InvokeRequired)
                    {
                        delegateUpdateMaxBar deleg = new delegateUpdateMaxBar(UpdateMaxBar);
                        _bar.Invoke(deleg, new object[] { value });
                    }
                    else
                    {
                        _bar.Maximum = value;
                        _bar.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _bar.Maximum = _bar.Maximum;
            }
        }

        public delegate void delegateUpdateColorBar(System.Drawing.Color value);
        public void UpdateColorBar(System.Drawing.Color value)
        {
            if (_bar != null)
            {
                if (_bar.InvokeRequired)
                {
                    delegateUpdateColorBar deleg = new delegateUpdateColorBar(UpdateColorBar);
                    _bar.Invoke(deleg, new object[] { value });
                }
                else
                {
                    _bar.ForeColor = value;
                }
            }
        }

        #endregion

        #region Label
        Label _lab;
        public ObjectThread(Label _lab_)
        {
            this._lab = _lab_;
        }

        public delegate void delegateTextLabel(string text);
        public void TextLabel(string text)
        {
            if (_lab != null)
            {
                if (_lab.InvokeRequired)
                {
                    delegateTextLabel deleg = new delegateTextLabel(TextLabel);
                    _lab.Invoke(deleg, new object[] { text });
                }
                else
                {
                    _lab.Text = text;
                }
            }
        }

        #endregion

        #region Thread

        #endregion
    }
}
