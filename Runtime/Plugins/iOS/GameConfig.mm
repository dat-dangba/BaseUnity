#import <Foundation/Foundation.h>

extern "C"
{
    const char* GetInfoPlistValue(const char* key)
    {
        @autoreleasepool {
            NSString* nsKey = [NSString stringWithUTF8String:key];
            NSString* value = [[[NSBundle mainBundle] infoDictionary] objectForKey:nsKey];
            if (value == nil) {
                return NULL;
            }
            return strdup([value UTF8String]); // strdup để trả về C string
        }
    }
}
