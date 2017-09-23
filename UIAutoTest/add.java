package com.example.nhy.mx6_60;

import android.app.Application;
import android.os.RemoteException;
import android.support.test.InstrumentationRegistry;
import android.support.test.runner.AndroidJUnit4;
import android.support.test.uiautomator.By;
import android.support.test.uiautomator.UiDevice;
import android.support.test.uiautomator.UiObject2;
import android.test.ApplicationTestCase;

import org.junit.Test;
import org.junit.runner.RunWith;

/**
 * <a href="http://d.android.com/tools/testing/testing_android.html">Testing Fundamentals</a>
 */
@RunWith(AndroidJUnit4.class)
public class ApplicationTest{
    public UiDevice device;

    @Test
    public void testDemo() throws RemoteException, InterruptedException {
        device=UiDevice.getInstance(InstrumentationRegistry.getInstrumentation());
getUiDevice().click(524,607);

	}
}