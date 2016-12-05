#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "ecclient.h"
#include <QMainWindow>
#include <QThreadPool>
#include <memory>

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private slots:
    void on_initButton_clicked();

    void on_uninitButton_clicked();

    void on_statusButton_clicked();

private:
    Ui::MainWindow *ui;
    std::unique_ptr<ECClient>        m_client;
    QThreadPool     m_pool;
};

#endif // MAINWINDOW_H
