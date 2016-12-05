#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>
#include <QJsonDocument>
#include <QJsonObject>
#include <functional>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    try
    {
        m_client.reset(new ECClient);
    }
    catch(...)
    {
        QMessageBox::critical(nullptr, "Error", "Cannot load EtcController.dll");
        ::exit(1);
    }
}

MainWindow::~MainWindow()
{
    delete ui;
}

class MyRunnable : public QRunnable
{
    std::function<void()> m_func;
public:
    template <typename T>
    MyRunnable(T&& t) : m_func(std::forward<T>(t)) {}

    virtual void run()
    {
        m_func();
    }
};

void MainWindow::on_initButton_clicked()
{
    m_pool.reserveThread();
    m_pool.start(new MyRunnable([this](){
        char conf[4096] = {0}, err[1024] = {0};
        int num = 0;
        int rc = m_client->Initialize(conf, &num, err);
        if(rc == 0)
        {
            QString config = QString::fromLocal8Bit(conf);
            QJsonObject obj = QJsonDocument::fromJson(config.toUtf8()).object();
            QMetaObject::invokeMethod(ui->userEdit, "setText", Qt::QueuedConnection, Q_ARG(QString, obj.take("UserName").toString()));
            QMetaObject::invokeMethod(ui->laneEdit, "setText", Qt::QueuedConnection, Q_ARG(QString, obj.take("LaneNo").toString()));
            QMetaObject::invokeMethod(ui->textEdit, "append", Qt::QueuedConnection, Q_ARG(QString, config));
        }
        else
        {
            QMetaObject::invokeMethod(ui->textEdit, "append", Qt::QueuedConnection, Q_ARG(QString, QString::fromLocal8Bit(err)));
        }
    }));
}

void MainWindow::on_uninitButton_clicked()
{
    m_pool.reserveThread();
    m_pool.start(new MyRunnable([this](){
        m_client->Uninstall();
    }));
}

void MainWindow::on_statusButton_clicked()
{
    QString user = ui->userEdit->text();
    int laneno = ui->laneEdit->text().toInt();
    m_pool.reserveThread();
    m_pool.start(new MyRunnable([=](){
        QJsonObject json;
        json.insert("UserName", user);

        char buf[4096] = {0};
        int rc = m_client->StatusQuery(laneno, QString(QJsonDocument(json).toJson()).toLocal8Bit().data(), buf);
        QMetaObject::invokeMethod(ui->textEdit, "append", Qt::QueuedConnection, Q_ARG(QString, QString::number(rc) + "\n" + QString::fromLocal8Bit(buf)));
    }));
}
