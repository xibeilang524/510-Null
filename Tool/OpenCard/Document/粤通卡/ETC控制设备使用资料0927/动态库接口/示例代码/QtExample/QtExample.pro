#-------------------------------------------------
#
# Project created by QtCreator 2016-10-08T10:13:57
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++11

TARGET = QtExample
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    ecclient.cpp

HEADERS  += mainwindow.h \
    ecclient.h

FORMS    += mainwindow.ui
