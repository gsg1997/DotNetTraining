"""
Create a menu driven program to take a movie name from the user and get its rating using selenium from an online source (IMBD) and other details

The menu should have options to save it to a file
when user press quit all details shld be saved to a file
"""

from selenium import webdriver
from selenium.webdriver.firefox.service import Service
from selenium.webdriver.common.by import By
import time
from selenium.webdriver.common.keys import Keys
import pandas as pd
import streamlit as st
import os

mainDF = pd.DataFrame()

#method to get movie details from IMDP. Method gets rating, director, writer and cast
def getMovieDetailsIMDP(movieName):
    #try:
        driver_path = r'geckodriver.exe'
        service = Service(driver_path)
        driver = webdriver.Firefox(service=service)
        driver.get("https://www.imdb.com/")
        driver.maximize_window()
        driver.implicitly_wait(3)

        searchBox = driver.find_element(By.XPATH,"""//*[@id="suggestion-search"]""")
        searchBox.send_keys(movieName)
        searchBox.submit()
        driver.implicitly_wait(3)
        
        movieTitleElements = driver.find_elements(By.CLASS_NAME,"ipc-metadata-list-summary-item__c")
        movieTitleElement = movieTitleElements[0]
        time.sleep(10)
        movieTitleElement.click()
        time.sleep(10)

        ratingElement = driver.find_element(By.XPATH,"//span[@class='sc-d541859f-1 imUuxf']")
        rating = ratingElement.text

        directorElement = driver.find_element(By.XPATH,"""//a[@class='ipc-metadata-list-item__list-content-item ipc-metadata-list-item__list-content-item--link']""")
        director = directorElement.text
        
        movieDF = pd.DataFrame({
                "Movie" : [movieName],
                "Director" : [director],
                "Rating" : [rating]
        })
        driver.quit()
        return movieDF
    #except Exception:
        #print("Exception occured")

#StreamLit Main

st.write("Menu")
option=st.selectbox("Select your Operation :",("Select Option","Get Movie Rating","View all movies searched"),)
print(option)
if option=="Get Movie Rating":
            movieName = st.text_input("Enter Movie name to get details : ","Movie Name")
            if movieName!="Movie Name":
                moviedf = getMovieDetailsIMDP(movieName)
                print(moviedf)
                if os.path.exists("Movie Rating.csv"):
                        moviedf.to_csv("Movie Rating.csv",mode="a", index=False, header=False)
                else:
                        moviedf.to_csv("Movie Rating.csv",index=False)
                st.dataframe(moviedf, use_container_width=True)

elif option=="View all movies searched":
            mainDF = pd.read_csv("Movie Rating.csv")
            st.dataframe(mainDF, use_container_width=True)

