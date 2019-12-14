import sys
import cv2
import numpy as np
from Hw2_ui import Ui_MainWindow
from matplotlib import pyplot as plt
from PyQt5.QtWidgets import QMainWindow, QApplication



class MainWindow(QMainWindow, Ui_MainWindow):
    def __init__(self, parent=None):
        super(MainWindow, self).__init__(parent)
        self.setupUi(self)
        self.onBindingUI()

    def onBindingUI(self):
        self.pushButton.clicked.connect(self.on_btn1_1_click)
        self.pushButton_2.clicked.connect(self.on_btn2_1_click)
        self.pushButton_3.clicked.connect(self.on_btn3_1_click)
        self.pushButton_4.clicked.connect(self.on_btn3_2_click)

    def on_btn1_1_click(self):

        imgL = cv2.imread('images\imL.png',0)
        imgR = cv2.imread('images\imR.png',0)

        stereo = cv2.StereoBM_create(numDisparities=64, blockSize=9)
        disparity = stereo.compute(imgL,imgR)
        plt.imshow(disparity,'gray')
        plt.show()

    def on_btn2_1_click(self):

        img_rgb = cv2.imread(r"images\ncc_img.jpg")
        img_gray = cv2.cvtColor(img_rgb, cv2.COLOR_BGR2GRAY)
        template = cv2.imread(r"images\ncc_template.jpg", cv2.IMREAD_GRAYSCALE)
        w, h = template.shape[::-1]

        res = cv2.matchTemplate(img_gray, template, cv2.TM_CCOEFF_NORMED)
        res = cv2.normalize(res, res, 0, 1, cv2.NORM_MINMAX, -1)
        loc = np.where(res >= 0.97)

        for pt in zip(*loc[::-1]):
            cv2.rectangle(img_rgb, pt, (pt[0] + w, pt[1] + h), (255, 0, 0), 2)


        cv2.imshow('Template matching feature', res)
        cv2.imshow('Detected',img_rgb)
        cv2.waitKey(0)
        cv2.destroyAllWindows()

    def on_btn3_1_click(self):
        print("還沒搞")

    def on_btn3_2_click(self):
        print("還沒搞")


if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())